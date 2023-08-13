using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Common.Constants;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.ValueObjects;

public class PhoneTypeConfiguration : IEntityTypeConfiguration<Phone>
{
    public void Configure(EntityTypeBuilder<Phone> builder)
    {
        builder.Property(phone => phone.Value)
            .HasMaxLength(StringLengths.PhoneNumber)
            .IsUnicode()
            .IsRequired();
    }
}
