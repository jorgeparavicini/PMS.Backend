using JetBrains.Annotations;
using PMS.Backend.Features.Agency.Events;
using PMS.Backend.Features.Exceptions;
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
    public IList<AgencyContact> Contacts => _contacts.AsReadOnly();

    /// <summary>
    ///   Required by EF Core.
    /// </summary>
    [UsedImplicitly]
    private Agency()
    {
        LegalName = null!;
        DefaultCommission = null;
        DefaultCommissionOnExtras = null;
        CommissionMethod = null!;
        EmergencyContact = null!;
        _contacts = new List<AgencyContact>();
    }

    public Agency(
        string legalName,
        decimal? defaultCommission,
        decimal? defaultCommissionOnExtras,
        int commissionMethod,
        string? email,
        string? phone,
        IList<AgencyContact> contacts)
    {
        LegalName = legalName;
        DefaultCommission = Commission.FromDecimal(defaultCommission);
        DefaultCommissionOnExtras = Commission.FromDecimal(defaultCommissionOnExtras);
        CommissionMethod = CommissionMethod.From(commissionMethod);
        EmergencyContact = ContactDetails.FromStrings(email, phone);
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
        decimal? defaultCommission,
        decimal? defaultCommissionOnExtras,
        int commissionMethod,
        string? emergencyEmail,
        string? emergencyPhone)
    {
        LegalName = legalName;
        DefaultCommission = Commission.FromDecimal(defaultCommission);
        DefaultCommissionOnExtras = Commission.FromDecimal(defaultCommissionOnExtras);
        CommissionMethod = CommissionMethod.From(commissionMethod);
        EmergencyContact = ContactDetails.FromStrings(emergencyEmail, emergencyPhone);
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

    public void UpdateContact(
        Guid contactId,
        string name,
        string? email,
        string? phone,
        string? street,
        string? city,
        string? state,
        string? country,
        string? zipCode)
    {
        AgencyContact? contact = _contacts.FirstOrDefault(contact => contact.Id == contactId);
        if (contact is null)
        {
            throw new NotFoundException<AgencyContact>(contactId);
        }

        contact.SetDetails(name, email, phone, street, city, state, country, zipCode);
    }

    public override void Delete()
    {
        base.Delete();

        foreach (AgencyContact agencyContact in Contacts)
        {
            agencyContact.Delete();
        }
    }

    public void DeleteContact(Guid contactId)
    {
        AgencyContact? contact = _contacts.FirstOrDefault(contact => contact.Id == contactId);
        if (contact is null)
        {
            throw new NotFoundException<AgencyContact>(contactId);
        }

        contact.Delete();
    }
}
