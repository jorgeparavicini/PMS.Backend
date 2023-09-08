using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class DateRangeTypeConfiguration
{
    internal static void ConfigureDateRange<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, DateRange?>> navigationExpression)
        where T : class
    {
        builder.OwnsOne(navigationExpression, ConfigureDateRange);
    }

    private static void ConfigureDateRange<T>(this OwnedNavigationBuilder<T, DateRange> builder)
        where T : class
    {
        builder.Property(e => e.StartDate)
            .IsRequired();

        builder.Property(e => e.EndDate)
            .IsRequired();
    }
}
