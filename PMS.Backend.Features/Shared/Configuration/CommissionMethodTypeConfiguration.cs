using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class CommissionMethodTypeConfiguration
{
    internal static void ConfigureCommissionMethod<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, CommissionMethod>> navigationExpression)
        where T : Entity
    {
        builder.Property(navigationExpression)
            .HasConversion(method => method.Id, id => CommissionMethod.From(id))
            .IsRequired();
    }
}
