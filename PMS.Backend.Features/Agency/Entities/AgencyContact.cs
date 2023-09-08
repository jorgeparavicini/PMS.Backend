using PMS.Backend.Features.Shared;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Entities;

internal class AgencyContact : Entity
{
    public RequiredString Name { get; private set; }
    public ContactDetails ContactDetails { get; private set; }
    public Address Address { get; private set; }

    private AgencyContact() : this(null!, null!, null!)
    {
    }

    public AgencyContact(string name, ContactDetails contactDetails, Address address)
    {
        Name = name;
        ContactDetails = contactDetails;
        Address = address;
    }

    public void SetDetails(string name, ContactDetails contactDetails, Address address)
    {
        Name = name;
        ContactDetails = contactDetails;
        Address = address;
    }
}
