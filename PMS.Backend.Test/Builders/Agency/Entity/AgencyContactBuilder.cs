using System;
using System.Collections.Generic;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Test.Builders.Agency.Entity;

public class AgencyContactBuilder
{
    private Guid _id;
    private string _contactName = "ContactName";
    private string? _email;
    private string? _phone;
    private string? _address;
    private string? _city;
    private string? _zipCode;
    private bool _isFrequentVendor;

    public AgencyContactBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public AgencyContactBuilder WithContactName(string contactName)
    {
        _contactName = contactName;
        return this;
    }

    public AgencyContactBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public AgencyContactBuilder WithPhone(string? phone)
    {
        _phone = phone;
        return this;
    }

    public AgencyContactBuilder WithAddress(string? address)
    {
        _address = address;
        return this;
    }

    public AgencyContactBuilder WithCity(string? city)
    {
        _city = city;
        return this;
    }

    public AgencyContactBuilder WithZipCode(string? zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public AgencyContactBuilder WithIsFrequentVendor(bool isFrequentVendor)
    {
        _isFrequentVendor = isFrequentVendor;
        return this;
    }

    public AgencyContact Build(Core.Entities.Agency.Agency agency, Guid agencyId)
    {
        return new AgencyContact
        {
            Id = _id,
            ContactName = _contactName,
            Email = _email,
            Phone = _phone,
            Address = _address,
            City = _city,
            ZipCode = _zipCode,
            IsFrequentVendor = _isFrequentVendor,
            AgencyId = agencyId,
            Agency = agency,
            GroupReservations = new List<GroupReservation>(),
        };
    }
}
