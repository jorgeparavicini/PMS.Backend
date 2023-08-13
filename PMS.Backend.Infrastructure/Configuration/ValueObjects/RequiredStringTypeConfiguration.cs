using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Common.Constants;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.ValueObjects;

public class RequiredStringTypeConfiguration : IEntityTypeConfiguration<RequiredString>
{
    public void Configure(EntityTypeBuilder<RequiredString> builder)
    {
        builder.Property(requiredString => requiredString.Value)
            .HasMaxLength(StringLengths.GeneralText)
            .IsUnicode()
            .IsRequired();
    }
}
