using System;
using PMS.Backend.Api.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class CreateAgencyContactInputBuilder
{
    private string _contactName = "ContactName";
    private string? _email;
    private string? _phone;
    private string? _address;
    private string? _city;
    private string? _zipCode;
    private bool _isFrequentVendor;
    private Guid _agencyId;

    public CreateAgencyContactInputBuilder WithContactName(string contactName)
    {
        _contactName = contactName;
        return this;
    }

    public CreateAgencyContactInputBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public CreateAgencyContactInputBuilder WithPhone(string? phone)
    {
        _phone = phone;
        return this;
    }

    public CreateAgencyContactInputBuilder WithAddress(string? address)
    {
        _address = address;
        return this;
    }

    public CreateAgencyContactInputBuilder WithCity(string? city)
    {
        _city = city;
        return this;
    }

    public CreateAgencyContactInputBuilder WithZipCode(string? zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public CreateAgencyContactInputBuilder WithIsFrequentVendor(bool isFrequentVendor)
    {
        _isFrequentVendor = isFrequentVendor;
        return this;
    }

    public CreateAgencyContactInputBuilder WithAgencyId(Guid agencyId)
    {
        _agencyId = agencyId;
        return this;
    }

    public CreateAgencyContactInput Build()
    {
        return new CreateAgencyContactInput
        {
            ContactName = _contactName,
            Email = _email,
            Phone = _phone,
            Address = _address,
            City = _city,
            ZipCode = _zipCode,
            IsFrequentVendor = _isFrequentVendor,
            AgencyId = _agencyId,
        };
    }
}
