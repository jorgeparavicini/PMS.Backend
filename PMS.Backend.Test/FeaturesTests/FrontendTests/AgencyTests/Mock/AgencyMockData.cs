using System.Collections.Generic;
using System.Net.Http;
using PMS.Backend.Common.Models;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Input;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

public static class AgencyMockData
{
    public static List<Agency> GetAgencies()
    {
        return new List<Agency>
        {
            new()
            {
                LegalName = "Agency1",
                DefaultCommissionRate = decimal.One,
                DefaultCommissionOnExtras = decimal.One,
                CommissionMethod = CommissionMethod.DeductedByAgency,
                EmergencyPhone = "EmergencyPhone1",
                EmergencyEmail = "mail@mail.com",
                AgencyContacts =
                {
                    new AgencyContact()
                    {
                        ContactName = "Contact1",
                        Email = "mail@mail.com",
                        Phone = "Phone1",
                        Address = "Address1",
                        City = "City1",
                        ZipCode = "Zip1",
                        IsFrequentVendor = true
                    },
                    new AgencyContact
                    {
                        ContactName = "Contact2",
                        Email = "mail@mail.com",
                        Phone = "Phone2",
                        Address = "Address2",
                        City = "City2",
                        ZipCode = "Zip2",
                        IsFrequentVendor = true
                    }
                },
            },
            new()
            {
                LegalName = "Agency2",
                DefaultCommissionRate = decimal.Zero,
                DefaultCommissionOnExtras = decimal.Zero,
                CommissionMethod = CommissionMethod.DeductedByProvider,
                EmergencyPhone = "EmergencyPhone2",
                EmergencyEmail = "mail2@mail.com",
            }
        };
    }

    public static CreateAgencyDTO GetCreateMockAgency() => new(
        "Legal Name",
        decimal.Zero,
        decimal.One,
        CommissionMethod.DeductedByProvider,
        "Phone",
        "mail@mail.com",
        new List<CreateAgencyContactDTO>(new[]
        {
            new CreateAgencyContactDTO("Contact",
                "email@email.com",
                null,
                "Address",
                "City",
                "Zip Code")
        }));

    public static Agency GetCreateMockAgencyAsAgency() => new()
    {
        LegalName = "Legal Name",
        DefaultCommissionRate = decimal.Zero,
        DefaultCommissionOnExtras = decimal.One,
        CommissionMethod = CommissionMethod.DeductedByProvider,
        EmergencyPhone = "Phone",
        EmergencyEmail = "mail@mail.com",
        AgencyContacts = new List<AgencyContact>()
        {
            new()
            {
                ContactName = "Contact",
                Email = "email@email.com",
                Phone = null,
                Address = "Address",
                City = "City",
                ZipCode = "Zip Code"
            }
        }
    };

    public static List<Agency> GetServiceAgencies() => new()
    {
        new Agency
        {
            Id = 1,
            LegalName = "Agency1",
            DefaultCommissionRate = decimal.One,
            DefaultCommissionOnExtras = decimal.One,
            CommissionMethod = CommissionMethod.DeductedByAgency,
            EmergencyPhone = "EmergencyPhone1",
            EmergencyEmail = "mail@mail.com",
            AgencyContacts =
            {
                new AgencyContact()
                {
                    Id = 1,
                    ContactName = "Contact1",
                    Email = "mail@mail.com",
                    Phone = "Phone1",
                    Address = "Address1",
                    City = "City1",
                    ZipCode = "Zip1",
                    IsFrequentVendor = true
                },
                new AgencyContact
                {
                    Id = 2,
                    ContactName = "Contact2",
                    Email = "mail@mail.com",
                    Phone = "Phone2",
                    Address = "Address2",
                    City = "City2",
                    ZipCode = "Zip2",
                    IsFrequentVendor = true
                }
            },
        },
        new Agency
        {
            Id = 2,
            LegalName = "Agency2",
            DefaultCommissionRate = decimal.Zero,
            DefaultCommissionOnExtras = decimal.Zero,
            CommissionMethod = CommissionMethod.DeductedByProvider,
            EmergencyPhone = "EmergencyPhone2",
            EmergencyEmail = "mail2@mail.com",
        }
    };

    public static IEnumerable<object[]> Endpoints => new List<object[]>()
    {
        new object[] { "agencies", HttpMethod.Get },
        new object[] { "agencies", HttpMethod.Post },
        new object[] { "agencies(1)", HttpMethod.Get },
        new object[] { "agencies(1)", HttpMethod.Put },
        new object[] { "agencies(1)", HttpMethod.Delete }
    };
}
