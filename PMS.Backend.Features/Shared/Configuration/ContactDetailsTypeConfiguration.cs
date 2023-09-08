using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Configuration;

internal static class ContactDetailsTypeConfiguration
{
    internal static void ConfigureContactDetails<T>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, ContactDetails?>> navigationExpression)
        where T : Entity
    {
        builder.OwnsOne(navigationExpression,
            contactDetails =>
            {
                contactDetails.ConfigureEmail(cd => cd.Email);

                contactDetails.ConfigurePhone(cd => cd.Phone);
            });
    }
}
