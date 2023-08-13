using System;
using System.Collections.Generic;
using System.Linq;
using PMS.Backend.Domain.Common;
using PMS.Backend.Domain.Events.Agency;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Domain.Aggregates.Agency;

public class Agency : Entity, IAggregateRoot
{
    private RequiredString _legalName;

    public string LegalName => _legalName;

    public Commission? DefaultCommission { get; private set; }

    public Commission? DefaultCommissionOnExtras { get; private set; }

    public CommissionMethod CommissionMethod { get; private set; }

    public ContactDetails EmergencyContact { get; private set; }

    private readonly List<AgencyContact> _agencyContacts = new();
    public IReadOnlyCollection<AgencyContact> AgencyContacts => _agencyContacts.AsReadOnly();

    public Agency(
        string legalName,
        Commission? defaultCommission,
        Commission? defaultCommissionOnExtras,
        CommissionMethod commissionMethod,
        ContactDetails contactDetails
    )
    {
        _legalName = new RequiredString(legalName, nameof(LegalName));
        DefaultCommission = defaultCommission;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyContact = contactDetails;


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
        _legalName = new RequiredString(legalName, nameof(LegalName));
        DefaultCommission = defaultCommission;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyContact = emergencyContact;
    }

    public void AddContact(AgencyContact contact)
    {
        _agencyContacts.Add(contact);
    }

    public void RemoveContact(AgencyContact contact)
    {
        _agencyContacts.Remove(contact);
    }

    public void UpdateContact(Guid contactId, string name, ContactDetails contactDetails, Address address)
    {
        AgencyContact? contact = _agencyContacts.FirstOrDefault(contact => contact.Id == contactId);
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

        foreach (AgencyContact agencyContact in AgencyContacts)
        {
            agencyContact.Delete();
        }
    }
}
