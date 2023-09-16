using JetBrains.Annotations;
using PMS.Backend.Features.Shared;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Entities;

internal class AgencyContact : Entity
{
    public RequiredString Name { get; private set; }
    public ContactDetails ContactDetails { get; private set; }
    public Address Address { get; private set; }

    [UsedImplicitly]
    private AgencyContact()
    {
        Name = null!;
        ContactDetails = null!;
        Address = null!;
    }

    public AgencyContact(
        string name,
        string? email,
        string? phone,
        string? street,
        string? city,
        string? state,
        string? country,
        string? zipCode)
    {
        Name = name;
        ContactDetails = ContactDetails.FromStrings(email, phone);
        Address = new Address(street, city, state, country, zipCode);
    }

    public void SetDetails(
        string name,
        string? email,
        string? phone,
        string? street,
        string? city,
        string? state,
        string? country,
        string? zipCode)
    {
        Name = name;
        ContactDetails = ContactDetails.FromStrings(email, phone);
        Address = new Address(street, city, state, country, zipCode);
    }
}
