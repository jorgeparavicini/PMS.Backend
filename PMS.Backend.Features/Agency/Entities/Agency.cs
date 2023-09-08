using PMS.Backend.Features.Agency.Events;
using PMS.Backend.Features.Shared;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Entities;

internal class Agency : Entity, IAggregateRoot
{
    public RequiredString LegalName { get; private set; }

    public Commission? DefaultCommission { get; private set; }

    public Commission? DefaultCommissionOnExtras { get; private set; }

    public CommissionMethod CommissionMethod { get; private set; }

    public ContactDetails EmergencyContact { get; private set; }

    private readonly IList<AgencyContact> _contacts;
    public IReadOnlyCollection<AgencyContact> Contacts => _contacts.AsReadOnly();

    private Agency()
    {
        // Required by EF Core
    }

    public Agency(
        string legalName,
        Commission? defaultCommission,
        Commission? defaultCommissionOnExtras,
        CommissionMethod commissionMethod,
        ContactDetails contactDetails,
        IList<AgencyContact> contacts)
    {
        if (contacts.Count == 0)
        {
            throw new ArgumentException("Agency must have at least one contact.", nameof(contacts));
        }

        LegalName = legalName;
        DefaultCommission = defaultCommission;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyContact = contactDetails;
        _contacts = contacts;


        AddDomainEvent(new AgencyCreatedEvent(
            Id,
            LegalName,
            DefaultCommission,
            DefaultCommissionOnExtras,
            CommissionMethod,
            EmergencyContact));
    }

    public void SetAgencyDetails(
        string legalName,
        Commission? defaultCommission,
        Commission? defaultCommissionOnExtras,
        CommissionMethod commissionMethod,
        ContactDetails emergencyContact)
    {
        LegalName = legalName;
        DefaultCommission = defaultCommission;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyContact = emergencyContact;
    }

    public void AddContact(AgencyContact contact)
    {
        _contacts.Add(contact);
    }

    public void RemoveContact(AgencyContact contact)
    {
        if (Contacts.Count == 1)
        {
            throw new InvalidOperationException("Agency must have at least one contact.");
        }

        _contacts.Remove(contact);
    }

    public void UpdateContact(Guid contactId, string name, ContactDetails contactDetails, Address address)
    {
        AgencyContact? contact = _contacts.FirstOrDefault(contact => contact.Id == contactId);
        if (contact is null)
        {
            throw new InvalidOperationException(
                $"Failed to update contact. No contact found with the specified ID: {contactId}.");
        }

        contact.SetDetails(name, contactDetails, address);
    }

    public override void Delete()
    {
        base.Delete();

        foreach (AgencyContact agencyContact in Contacts)
        {
            agencyContact.Delete();
        }
    }
}
