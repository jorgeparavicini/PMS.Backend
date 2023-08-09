using System;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class EditAgencyContactInputBuilder
{
    private Guid _id;
    private string _contactName = "ContactName";
    private string? _email;
    private string? _phone;
    private string? _address;
    private string? _city;
    private string? _zipCode;
    private bool _isFrequentVendor;

    public EditAgencyContactInputBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public EditAgencyContactInputBuilder WithContactName(string contactName)
    {
        _contactName = contactName;
        return this;
    }

    public EditAgencyContactInputBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public EditAgencyContactInputBuilder WithPhone(string? phone)
    {
        _phone = phone;
        return this;
    }

    public EditAgencyContactInputBuilder WithAddress(string? address)
    {
        _address = address;
        return this;
    }

    public EditAgencyContactInputBuilder WithCity(string? city)
    {
        _city = city;
        return this;
    }

    public EditAgencyContactInputBuilder WithZipCode(string? zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public EditAgencyContactInputBuilder WithIsFrequentVendor(bool isFrequentVendor)
    {
        _isFrequentVendor = isFrequentVendor;
        return this;
    }

    public EditAgencyContactInput Build()
    {
        return new EditAgencyContactInput
        {
            Id = _id,
            ContactName = _contactName,
            Email = _email,
            Phone = _phone,
            Address = _address,
            City = _city,
            ZipCode = _zipCode,
            IsFrequentVendor = _isFrequentVendor,
        };
    }
}
