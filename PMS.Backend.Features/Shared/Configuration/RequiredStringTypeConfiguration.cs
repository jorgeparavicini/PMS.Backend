using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class RequiredStringTypeConfiguration
{
    internal static void ConfigureRequiredString<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, RequiredString?>> navigationExpression,
        int maxLength = StringLengths.GeneralText)
        where T : class
    {
        builder.OwnsOne(navigationExpression, b => b.ConfigureRequiredString(maxLength));
    }

    internal static void ConfigureRequiredString<TOwnerEntity, TDependentEntity>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder,
        Expression<Func<TDependentEntity, RequiredString?>> navigationExpression,
        int maxLength = StringLengths.GeneralText)
        where TOwnerEntity : class
        where TDependentEntity : class
    {
        builder.OwnsOne(navigationExpression, b => b.ConfigureRequiredString(maxLength));
    }

    private static void ConfigureRequiredString<T>(
        this OwnedNavigationBuilder<T, RequiredString> builder,
        int maxLength)
        where T : class
    {
        builder.Property(p => p.Value)
            .HasMaxLength(maxLength)
            .IsRequired();
    }
}
