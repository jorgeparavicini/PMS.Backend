using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.ValueObjects;

public class DateRangeTypeConfiguration : IEntityTypeConfiguration<DateRange>
{
    public void Configure(EntityTypeBuilder<DateRange> builder)
    {
        builder.Property(dateRange => dateRange.StartDate)
            .IsRequired();

        builder.Property(dateRange => dateRange.EndDate)
            .IsRequired();
    }
}
