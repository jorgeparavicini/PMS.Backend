namespace PMS.Backend.Features.Shared.ValueObjects;

internal record ContactDetails(Email? Email = null, Phone? Phone = null)
{
    private ContactDetails() : this(null, null)
    {
    }
}
