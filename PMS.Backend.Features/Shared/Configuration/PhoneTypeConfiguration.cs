using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class PhoneTypeConfiguration
{
    internal static void ConfigurePhone<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, Phone?>> navigationExpression)
        where T : class
    {
        builder.OwnsOne(navigationExpression, ConfigurePhone);
    }

    internal static void ConfigurePhone<TOwnerEntity, TDependentEntity>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder,
        Expression<Func<TDependentEntity, Phone?>> navigationExpression)
        where TOwnerEntity : class
        where TDependentEntity : class
    {
        builder.OwnsOne(navigationExpression, ConfigurePhone);
    }

    private static void ConfigurePhone<T>(this OwnedNavigationBuilder<T, Phone> builder)
        where T : class
    {
        builder.Property(p => p.Value)
            .HasMaxLength(StringLengths.PhoneNumber)
            .IsUnicode(false)
            .IsRequired();
    }
}
