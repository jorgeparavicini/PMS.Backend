using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Validators;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Test;

public static class TestExtensions
{
    public static void AssertEqualValidation<TEntity, TModel, TValidator>(bool isOutput = false)
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
                // Output models do not have to be marked as required.
                if (entityAttribute.AttributeType == typeof(NullableAttribute) && !isOutput)
                {
                    AssertModelProperty_IsNotNull<TModel>(modelProperty, modelValidators);
                    continue;
                }

                // If the entity has a max length validator, the model should have one with the
                // same max size.
                if (entityAttribute.AttributeType == typeof(MaxLengthAttribute))
                {
                    var maxLength = (int)entityAttribute.ConstructorArguments[0].Value!;
                    AssertModelProperty_HasSameMaxLength<TModel>(modelProperty, modelValidators,
                        maxLength);
                    continue;
                }

                // If the entity has a min length validator, the model should have on with the
                // same min size.
                if (entityAttribute.AttributeType == typeof(MinLengthAttribute))
                {
                    var minLength = (int)entityAttribute.ConstructorArguments[0].Value!;
                    AssertModelProperty_HasSameMinLength<TModel>(modelProperty, modelValidators,
                        minLength);
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
                "because the entity is required");
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
