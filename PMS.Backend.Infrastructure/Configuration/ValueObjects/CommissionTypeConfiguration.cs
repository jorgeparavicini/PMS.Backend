using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.ValueObjects;

public class CommissionTypeConfiguration : IEntityTypeConfiguration<Commission>
{
    public void Configure(EntityTypeBuilder<Commission> builder)
    {
        builder.Property(commission => commission.Value).HasPrecision(5, 4).IsRequired();
    }
}
