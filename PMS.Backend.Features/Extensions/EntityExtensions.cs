using System.Reflection;
using System.Runtime.ExceptionServices;
using Detached.Annotations;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Exceptions;

namespace PMS.Backend.Features.Extensions;

/// <summary>
/// Extensions for Entity types.
/// </summary>
/// TODO: Use source generators to improve performance.
public static class EntityExtensions
{
    private static readonly MethodInfo ValidateIdMethod = typeof(EntityExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .First(x => x.Name == nameof(ValidateId));

    private static readonly MethodInfo ValidateAggregateEnumerableMethod = typeof(EntityExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .First(x => x.Name == nameof(ValidateAggregateEnumerable));

    private static readonly MethodInfo ValidateCompositeEnumerableMethod = typeof(EntityExtensions)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
        .First(x => x.Name == nameof(ValidateCompositeEnumerable));

    /// <summary>
    /// Validates the IDs for all aggregate and composite relations in a property.
    /// </summary>
    /// <param name="entity">The entity to validate.</param>
    /// <param name="context">The database context where this entity is part of.</param>
    /// <typeparam name="T">The type of the entity.</typeparam>
    /// <typeparam name="TContext">The type of the <see cref="DbContext"/>.</typeparam>
    public static void ValidateIds<T, TContext>(this T entity, TContext context)
        where T : Entity
        where TContext : DbContext
    {
        try
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                if (property.GetValue(entity) == null)
                {
                    continue;
                }

                if (property.PropertyType.IsSubclassOf(typeof(Entity)))
                {
                    ValidateEntity(property, context, entity);
                }
                else if (typeof(IEnumerable<Entity>).IsAssignableFrom(property.PropertyType))
                {
                    ValidateEnumerable(property, context, entity);
                }
            }
        }
        catch (TargetInvocationException e)
        {
            ExceptionDispatchInfo.Capture(e.InnerException!).Throw();
        }
    }

    private static void ValidateEntity<TContext>(
        PropertyInfo property,
        TContext context,
        Entity entity)
        where TContext : DbContext
    {
        if (property.GetCustomAttributes(false).Any(x => x is AggregationAttribute))
        {
            ValidateAggregate(property, context, entity);
        }
        else if (property.GetCustomAttributes(false)
                 .Any(x => x is CompositionAttribute))
        {
            ValidateComposite(property, context, entity);
        }
    }

    private static void ValidateAggregate<TContext>(
        PropertyInfo property,
        TContext context,
        Entity entity)
        where TContext : DbContext
    {
        if (entity.GetType().GetProperty($"{property.Name}Id") is { } idProperty)
        {
            ValidateIdMethod
                .MakeGenericMethod(property.PropertyType, context.GetType())
                .Invoke(null, new[] { context, idProperty.GetValue(entity) });
        }
    }

    private static void ValidateComposite<TContext>(
        PropertyInfo property,
        TContext context,
        Entity entity)
        where TContext : DbContext
    {
        var id = (property.GetValue(entity) as Entity)!.Id;
        ValidateIdMethod.MakeGenericMethod(property.PropertyType, context.GetType())
            .Invoke(null, new[] { context, id as object });
    }

    private static void ValidateEnumerable<TContext>(
        PropertyInfo property,
        TContext context,
        Entity parentEntity)
        where TContext : DbContext
    {
        var entities = property.GetValue(parentEntity);
        var enumerableType = entities!.GetType().GetGenericArguments()[0];
        if (property.GetCustomAttributes(false).Any(x => x is AggregationAttribute))
        {
            ValidateAggregateEnumerableMethod.MakeGenericMethod(enumerableType, typeof(TContext))
                .Invoke(null, new[] { context, entities });
        }
        else if (property.GetCustomAttributes(false).Any(x => x is CompositionAttribute))
        {
            ValidateCompositeEnumerableMethod.MakeGenericMethod(enumerableType, typeof(TContext))
                .Invoke(null, new[] { context, entities });
        }
    }

    private static void ValidateAggregateEnumerable<T, TContext>(
        TContext context,
        IEnumerable<T> entities)
        where T : Entity
        where TContext : DbContext
    {
        foreach (var entity in entities)
        {
            ValidateId<T, TContext>(context, entity.Id);
        }
    }

    private static void ValidateCompositeEnumerable<T, TContext>(
        TContext context,
        IEnumerable<T> entities)
        where T : Entity
        where TContext : DbContext
    {
        foreach (var entity in entities)
        {
            ValidateId<T, TContext>(context, entity.Id);
            entity.ValidateIds(context);
        }
    }

    private static void ValidateId<T, TContext>(TContext context, int id)
        where T : Entity
        where TContext : DbContext
    {
        if (id == 0) return;
        if (!context.Set<T>().Any(x => x.Id == id))
        {
            throw new NotFoundException(
                $"Could not find an entity of type {typeof(T).Name} with id {id}");
        }
    }
}
