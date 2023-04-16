using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Core.Configuration.Agency;

public class AgencyTypeConfiguration : EntityTypeConfiguration<Entities.Agency.Agency>
{
    public override void Configure(EntityTypeBuilder<Entities.Agency.Agency> builder)
    {
        base.Configure(builder);

        builder.Property(type => type.LegalName).HasMaxLength(255);
    }
}
