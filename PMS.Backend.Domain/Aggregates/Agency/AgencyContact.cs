using PMS.Backend.Domain.Common;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Domain.Aggregates.Agency;

public class AgencyContact : Entity
{
    private RequiredString _name;

    public string Name => _name;
    public ContactDetails ContactDetails { get; private set; }
    public Address Address { get; private set; }

    public AgencyContact(string name, ContactDetails contactDetails, Address address)
    {
        _name = new RequiredString(name, nameof(Name));
        ContactDetails = contactDetails;
        Address = address;
    }

    public void SetDetails(string name, ContactDetails contactDetails, Address address)
    {
        _name = new RequiredString(name, nameof(Name));
        ContactDetails = contactDetails;
        Address = address;
    }
}
