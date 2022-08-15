using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Validators;
using PMS.Backend.Core.Entities;
using PMS.Backend.Test.Assertions;

namespace PMS.Backend.Test.Extensions;

// TODO: Include Email address check
// TODO: Move to assertions
public static class TestExtensions
{
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
            var entityAttributes = EntityAssertions<TEntity>.GetProperties()
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

            // If the entity does not have a nullable attribute it means it's a required property
            // If a property has a default attribute value it is ok for the input to be null.
            // A property is nullable if it has the nullable attribute and its first
            // constructor argument is 1.
            if (entityAttributes!.Any(x =>
                    x.AttributeType == typeof(NullableAttribute) &&
                    x.ConstructorArguments[0].Value == (object)1) &&
                entityAttributes!.All(x => x.AttributeType != typeof(DefaultValueAttribute)))
            {
                AssertModelProperty_IsNotNull<TModel>(modelProperty, modelValidators);
            }

            foreach (var entityAttribute in entityAttributes!)
            {
                // If the entity has a max length validator, the model should have one with the
                // same max size.
                if (entityAttribute.AttributeType == typeof(MaxLengthAttribute))
                {
                    var maxLength = (int)entityAttribute.ConstructorArguments[0].Value!;
                    AssertModelProperty_HasSameMaxLength<TModel>(modelProperty,
                        modelValidators,
                        maxLength);
                    continue;
                }

                // If the entity has a min length validator, the model should have on with the
                // same min size.
                if (entityAttribute.AttributeType == typeof(MinLengthAttribute))
                {
                    var minLength = (int)entityAttribute.ConstructorArguments[0].Value!;
                    AssertModelProperty_HasSameMinLength<TModel>(modelProperty,
                        modelValidators,
                        minLength);
                }
            }
        }
    }

    private static void AssertModelProperty_IsNotNull<TModel>(
        PropertyInfo modelProperty,
        List<IPropertyValidator> modelValidators)
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
                $"because the entity {modelProperty.Name} is required");
    }

    private static void AssertModelProperty_HasSameMaxLength<TModel>(
        PropertyInfo modelProperty,
        List<IPropertyValidator> modelValidators,
        int maxLength)
    {
        var maxLengthValidator = modelValidators
            .OfType<MaximumLengthValidator<TModel>>()
            .FirstOrDefault();

        if (modelValidators.Find(x => x.GetType() == typeof(LengthValidator<TModel>)) is
            LengthValidator<TModel> lengthValidator)
        {
            lengthValidator.Max.Should()
                .Be(maxLength,
                    $"because the property {modelProperty.Name} of the entity " +
                    "declared it as such");

            return;
        }

        maxLengthValidator.Should()
            .NotBeNull(
                $"because the property {modelProperty.Name} of the entity has a" +
                " max length validator");

        maxLengthValidator!.Max.Should()
            .Be(maxLength,
                $"because the property {modelProperty.Name} of the entity declared" +
                " it as such");
    }

    private static void AssertModelProperty_HasSameMinLength<TModel>(
        PropertyInfo modelProperty,
        List<IPropertyValidator> modelValidators,
        int minLength)
    {
        var notEmptyValidatorType =
            typeof(NotEmptyValidator<,>).MakeGenericType(
                typeof(TModel),
                modelProperty.PropertyType);

        if (minLength == 1 &&
            modelValidators.Any(x => x.GetType() == notEmptyValidatorType))
        {
            return;
        }

        if (minLength == 1 &&
            modelValidators.Find(x => x.GetType() == typeof(LengthValidator<TModel>)) is
                LengthValidator<TModel> lengthValidator)
        {
            lengthValidator.Min.Should()
                .Be(minLength,
                    $"because the property {modelProperty.Name} of the entity" +
                    $" declared it as such");
            return;
        }

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
