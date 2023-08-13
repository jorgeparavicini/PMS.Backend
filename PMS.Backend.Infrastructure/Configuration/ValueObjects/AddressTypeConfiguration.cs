using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Common.Constants;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.ValueObjects;

public class AddressTypeConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(address => address.Street)
            .HasMaxLength(StringLengths.Address)
            .IsUnicode();

        builder.Property(address => address.City)
            .HasMaxLength(StringLengths.City)
            .IsUnicode();

        builder.Property(address => address.State)
            .HasMaxLength(StringLengths.State)
            .IsUnicode();

        builder.Property(address => address.Country)
            .HasMaxLength(StringLengths.Country)
            .IsUnicode();

        builder.Property(address => address.ZipCode)
            .HasMaxLength(StringLengths.PostalCode)
            .IsUnicode(false);
    }
}
