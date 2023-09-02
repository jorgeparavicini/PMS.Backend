using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Test.Builders.Agency.Models.Input;

public class CreateAgencyWithContactsAgencyContactInputBuilder
{
    private string _contactName = "ContactName";
    private string? _email;
    private string? _phone;
    private string? _address;
    private string? _city;
    private string? _zipCode;
    private bool _isFrequentVendor;

    public CreateAgencyWithContactsAgencyContactInputBuilder WithContactName(string contactName)
    {
        _contactName = contactName;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInputBuilder WithEmail(string? email)
    {
        _email = email;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInputBuilder WithPhone(string? phone)
    {
        _phone = phone;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInputBuilder WithAddress(string? address)
    {
        _address = address;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInputBuilder WithCity(string? city)
    {
        _city = city;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInputBuilder WithZipCode(string? zipCode)
    {
        _zipCode = zipCode;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInputBuilder WithIsFrequentVendor(bool isFrequentVendor)
    {
        _isFrequentVendor = isFrequentVendor;
        return this;
    }

    public CreateAgencyWithContactsAgencyContactInput Build()
    {
        return new CreateAgencyWithContactsAgencyContactInput
        {
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
