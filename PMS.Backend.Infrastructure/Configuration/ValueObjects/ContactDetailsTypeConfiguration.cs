using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Persistence.Configuration.ValueObjects;

public class ContactDetailsTypeConfiguration : IEntityTypeConfiguration<ContactDetails>
{
    public void Configure(EntityTypeBuilder<ContactDetails> builder)
    {
    }
}
