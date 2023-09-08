using FluentValidation;
using PMS.Backend.Features.Shared;

namespace PMS.Backend.Features.Extensions;

internal static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, decimal?> IsCommission<T>(this IRuleBuilder<T, decimal?> ruleBuilder)
    {
        return ruleBuilder.PrecisionScale(5, 4, true).InclusiveBetween(0.0m, 1.0m);
    }

    public static IRuleBuilderOptions<TInput, string> IsInEnum<TInput, TEnum>(
        this IRuleBuilder<TInput, string> ruleBuilder)
        where TEnum : Enumeration
    {
        return ruleBuilder.Must(input => Enumeration.GetAll<TEnum>().Any(enumeration => enumeration.Name == input));
    }
}
