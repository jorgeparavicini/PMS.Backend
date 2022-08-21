using System.Collections;
using System.Reflection;
using Detached.Annotations;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using PMS.Backend.Core.Entities;
using PMS.Backend.Test.Extensions;
using PMS.Backend.Test.Utils;

namespace PMS.Backend.Test.Assertions;

public class EntityAssertions<TEntity> : ReferenceTypeAssertions<TEntity, EntityAssertions<TEntity>>
    where TEntity : Entity
{
    public EntityAssertions(TEntity? subject)
#pragma warning disable CS8604
        // Fluent assertions accepts null values but does not mark the subject as nullable.
        : base(subject)
#pragma warning restore CS8604
    {
    }

    protected override string Identifier => "entity";

    public AndConstraint<EntityAssertions<TEntity>> BeEquivalentTo(
        TEntity other,
        string because = "",
        params object[] becauseArgs)
    {
        var constraint = new AndConstraint<EntityAssertions<TEntity>>(this);

        var properties = GetProperties();
        foreach (var propertyInfo in properties)
        {
            // Ignore properties that contain an ignored attribute.
            if (propertyInfo.CustomAttributes.Any(x =>
                    StaticData.IgnoredTypes.Contains(x.AttributeType)))
            {
                continue;
            }

            // Composite types should be handled on their own.
            if (propertyInfo.CustomAttributes.Any(x =>
                    x.AttributeType == typeof(CompositionAttribute)))
            {
                CompositeTypeShouldBeEquivalent(propertyInfo, other, because, becauseArgs);
                continue;
            }

            propertyInfo.GetValue(Subject).Should().BeEquivalentTo(propertyInfo.GetValue(other));
        }

        return constraint;
    }

    private void CompositeTypeShouldBeEquivalent(
        PropertyInfo propertyInfo,
        TEntity other,
        string because,
        params object[] becauseArgs)
    {
        var type = propertyInfo.PropertyType;
        if (type.IsTypeOf<Entity>())
        {
            // TODO: Test once there is a 1 - 1 entity relation.
            var compositeEntity = propertyInfo.GetValue(Subject)!;
            var otherEntity = propertyInfo.GetValue(other)!;

            typeof(EntityAssertions<>).MakeGenericType(type)
                .GetMethod(nameof(AssertEqualSubEntity))!
                .Invoke(null, new[] { compositeEntity, otherEntity });
            return;
        }

        if (type.GetGenericTypeDefinition() == typeof(IList<>) &&
            type.GetGenericArguments().SingleOrDefault() is { } genericType &&
            genericType.IsTypeOf<Entity>())
        {
            var compositeEntities = propertyInfo.GetValue(Subject) as IList;
            var otherEntities = propertyInfo.GetValue(other) as IList;

            if (compositeEntities is null)
            {
                otherEntities.Should().BeNull();
                return;
            }

            compositeEntities.Count.Should().Be(otherEntities!.Count);
            for (var i = 0; i < compositeEntities.Count; i++)
            {
                typeof(EntityAssertions<>)
                    .MakeGenericType(genericType)
                    .GetMethod(nameof(AssertEqualSubEntity),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .Invoke(null, new[] { compositeEntities[i]!, otherEntities[i]! });
            }

            return;
        }

        Execute.Assertion
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected composite property {0} to be either an Entity or an Enumerable over " +
                "Entity{reason}, but found {1}",
                propertyInfo.Name,
                propertyInfo.PropertyType.Name);
    }

    private static AndConstraint<EntityAssertions<TEntity>> AssertEqualSubEntity(
        TEntity subject,
        TEntity other)
    {
        return subject.Should().BeEquivalentTo(other);
    }

    public static IList<PropertyInfo> GetProperties()
    {
        // We don't want to compare the base entity properties as they are auto generated
        // and don't need to be validated except the Id.
        var omittedProperties = typeof(Entity).GetProperties()
            .Select(x => x.Name)
            .Where(x => x != nameof(Entity.Id));
        return typeof(TEntity)
            .GetProperties()
            .Where(x => !omittedProperties.Contains(x.Name))
            .ToList();
    }
}
