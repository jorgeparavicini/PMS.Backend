using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper.Execution;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Validators;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Test;

public static class TestExtensions
{
    private static IList<IPropertyValidator> GetValidatorsForMember<T, TProperty>(
        this IValidator<T> validator,
        Expression<Func<T, TProperty>> expression)
    {
        var descriptor = validator.CreateDescriptor();
        var expressionMemberName = expression.GetMember()?.Name;

        return descriptor.GetValidatorsForMember(expressionMemberName)
            .Select(x => x.Validator)
            .ToList();
    }

    public static void AssertEqualValidation<TEntity, TModel, TValidator>()
        where TEntity : Entity
        where TValidator : AbstractValidator<TModel>, new()
    {
        var validatorContext = new TValidator();
        var descriptor = validatorContext.CreateDescriptor();

        var modelProperties = typeof(TModel).GetProperties();
        foreach (var modelProperty in modelProperties)
        {
            // Get all the attributes for the entity property
            var entityAttributes = GetProperties<TEntity>()
                .Where(x => x.Name == modelProperty.Name)
                .Select(x => x.CustomAttributes.ToList())
                .FirstOrDefault();

            // Verify the name of the entity property is the same as the model property
            entityAttributes.Should()
                .NotBeNull(
                    $"because the property {modelProperty.Name} exists on the model but not on the entity");

            // Get all validators for the model
            var modelValidators = descriptor.GetValidatorsForMember(modelProperty.Name)
                .Select(x => x.Validator)
                .ToList();

            foreach (var entityAttribute in entityAttributes!)
            {
                // If the entity has a nullable attribute it means it's a required property
                if (entityAttribute.AttributeType == typeof(NullableAttribute))
                {
                    var notEmptyValidatorType =
                        typeof(NotEmptyValidator<,>).MakeGenericType(
                            typeof(TModel),
                            modelProperty.PropertyType);

                    var notNullValidatorType =
                        typeof(NotNullValidator<,>).MakeGenericType(typeof(TModel),
                            modelProperty.PropertyType);

                    modelValidators.Should()
                        .Contain(x =>
                            x.GetType() == notEmptyValidatorType ||
                            x.GetType() == notNullValidatorType,
                            "because the entity is required");
                }

                // If the entity has a max length validator, the model should have one with the
                // same max size.
                if (entityAttribute.AttributeType == typeof(MaxLengthAttribute))
                {
                    var maxLength = (int)entityAttribute.ConstructorArguments[0].Value!;
                    var maxLengthValidator = modelValidators
                        .OfType<MaximumLengthValidator<TModel>>()
                        .FirstOrDefault();

                    maxLengthValidator.Should()
                        .NotBeNull(
                            $"because the property {modelProperty.Name} of the entity has a" +
                            " max length validator");

                    maxLengthValidator!.Max.Should()
                        .Be(maxLength,
                            $"because the property {modelProperty.Name} of the entity declared" +
                            " it as such");
                }

                // If the entity has a min length validator, the model should have on with the
                // same min size.
                if (entityAttribute.AttributeType == typeof(MinLengthAttribute))
                {
                    var minLength = (int)entityAttribute.ConstructorArguments[0].Value!;
                    var notEmptyValidatorType =
                        typeof(NotEmptyValidator<,>).MakeGenericType(
                            typeof(TModel),
                            modelProperty.PropertyType);

                    if (!(minLength == 1 &&
                          modelValidators.Any(x => x.GetType() == notEmptyValidatorType)))
                    {
                        var minLengthValidator = modelValidators
                            .OfType<MinimumLengthValidator<TModel>>()
                            .FirstOrDefault();

                        minLengthValidator.Should()
                            .NotBeNull(
                                $"because the property {modelProperty.Name} of the entity has a" +
                                $" min length validator");


                        minLengthValidator!.Min.Should()
                            .Be(minLength,
                                $"because the property {modelProperty.Name} of the entity declared" +
                                $" it as such");
                    }
                }
            }
        }
    }

    private static IList<PropertyInfo> GetProperties<T>() where T : Entity
    {
        // We don't want to compare the base entity properties as they are auto generated
        // and don't need to be validated.
        var omittedProperties = typeof(Entity).GetProperties().Select(x => x.Name);
        return typeof(T)
            .GetProperties()
            .Where(x => !omittedProperties.Contains(x.Name))
            .ToList();
    }
}
