using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class AddressTypeConfiguration
{
    internal static void ConfigureAddress<T>(this EntityTypeBuilder<T> builder,  Expression<Func<T, Address?>> navigationExpression)
        where T : Entity
    {
        builder.OwnsOne(navigationExpression, address =>
        {
            address.Property(a => a.Street)
                .HasMaxLength(StringLengths.Address)
                .IsUnicode();

            address.Property(a => a.City)
                .HasMaxLength(StringLengths.City)
                .IsUnicode();

            address.Property(a => a.State)
                .HasMaxLength(StringLengths.State)
                .IsUnicode();

            address.Property(a => a.Country)
                .HasMaxLength(StringLengths.Country)
                .IsUnicode();

            address.Property(a => a.ZipCode)
                .HasMaxLength(StringLengths.PostalCode)
                .IsUnicode(false);
        });
    }
}
