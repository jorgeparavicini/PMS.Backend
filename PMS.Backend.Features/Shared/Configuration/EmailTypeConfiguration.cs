using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class EmailTypeConfiguration
{
    public static void ConfigureEmail<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, Email?>> navigationExpression)
        where T : class
    {
        builder.OwnsOne(navigationExpression, ConfigureEmail);
    }

    public static void ConfigureEmail<TOwnerEntity, TDependentEntity>(
        this OwnedNavigationBuilder<TOwnerEntity, TDependentEntity> builder,
        Expression<Func<TDependentEntity, Email?>> navigationExpression)
        where TOwnerEntity : class
        where TDependentEntity : class
    {
        builder.OwnsOne(navigationExpression, ConfigureEmail);
    }

    private static void ConfigureEmail<T>(this OwnedNavigationBuilder<T, Email> builder)
        where T : class
    {
        builder.Property(e => e.Value)
            .HasMaxLength(StringLengths.Email)
            .IsUnicode(false)
            .IsRequired();
    }
}
