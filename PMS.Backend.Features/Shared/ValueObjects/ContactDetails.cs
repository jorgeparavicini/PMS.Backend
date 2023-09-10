namespace PMS.Backend.Features.Shared.ValueObjects;

internal record ContactDetails(Email? Email = null, Phone? Phone = null)
{
    private ContactDetails() : this(null, null)
    {
    }

    public static ContactDetails FromStrings(string? email, string? phone)
        => new ContactDetails(Email.FromString(email), Phone.FromString(phone));
}
