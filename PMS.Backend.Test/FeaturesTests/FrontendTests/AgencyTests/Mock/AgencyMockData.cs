using PMS.Backend.Common.Models;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

public static class AgencyMockData
{
    public static List<Agency> GetAgencies()
    {
        return new List<Agency>
        {
            new()
            {
                Id = 1,
                LegalName = "Agency1",
                DefaultCommissionRate = decimal.One,
                DefaultCommissionOnExtras = decimal.One,
                CommissionMethod = CommissionMethod.DeductedByAgency,
                EmergencyPhone = "EmergencyPhone1",
                EmergencyEmail = "EmergencyEmail1",
                AgencyContacts =
                {
                    new AgencyContact()
                    {
                        Id = 1,
                        ContactName = "Contact1",
                        Email = "Email1",
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
                        Email = "Email2",
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
                Id = 2,
                LegalName = "Agency2",
                DefaultCommissionRate = decimal.Zero,
                DefaultCommissionOnExtras = decimal.Zero,
                CommissionMethod = CommissionMethod.DeductedByProvider,
                EmergencyPhone = "EmergencyPhone2",
                EmergencyEmail = "EmergencyEmail2",
            }
        };
    }

    public static IEnumerable<object[]> Endpoints => new List<object[]>()
    {
        new object[] { "agencies", HttpMethod.Get },
        new object[] { "agencies", HttpMethod.Post },
        new object[] { "agencies(1)", HttpMethod.Get },
        new object[] { "agencies(1)", HttpMethod.Put },
        new object[] { "agencies(1)", HttpMethod.Delete }
    };
}
