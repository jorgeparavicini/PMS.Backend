using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class CommissionTypeConfiguration
{
    internal static void ConfigureCommission<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, Commission?>> navigationExpression)
        where T : Entity
    {
        builder.OwnsOne(navigationExpression, commission =>
        {
            commission.Property(c => c.Value).HasPrecision(5, 4).IsRequired();
        });
    }
}
