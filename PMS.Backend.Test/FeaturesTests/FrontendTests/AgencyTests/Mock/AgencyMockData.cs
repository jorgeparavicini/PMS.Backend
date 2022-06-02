using AutoMapper;
using PMS.Backend.Common.Models;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

public static class AgencyMockData
{
    private static MapperConfiguration Config { get; } = new(cfg =>
    {
        cfg.CreateMap<Agency, AgencySummaryDTO>();
        cfg.CreateMap<Agency, AgencyDetailDTO>();
        cfg.CreateMap<Agency, CreateAgencyDTO>();
        cfg.CreateMap<Agency, UpdateAgencyDTO>();
        cfg.CreateMap<AgencyContact, AgencyContactDTO>();
        cfg.CreateMap<AgencyContactDTO, UpdateAgencyContactDTO>();
        cfg.CreateMap<AgencyContactDTO, CreateAgencyContactDTO>();
    });

    private static IMapper Mapper { get; } = Config.CreateMapper();

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

    public static IEnumerable<AgencySummaryDTO> GetEmptyAgencies()
    {
        return new List<AgencySummaryDTO>();
    }

    public static IEnumerable<AgencySummaryDTO> GetAgencySummaries()
    {
        return Mapper.Map<List<Agency>, List<AgencySummaryDTO>>(GetAgencies());
    }

    public static AgencyDetailDTO GetAgencyDetail()
    {
        return Mapper.Map<Agency, AgencyDetailDTO>(GetAgencies().First());
    }

    public static CreateAgencyDTO GetCreateAgencyDTO()
    {
        return Mapper.Map<Agency, CreateAgencyDTO>(GetAgencies().First());
    }

    public static AgencySummaryDTO GetCreatedAgencySummary()
    {
        return Mapper.Map<Agency, AgencySummaryDTO>(GetAgencies().First());
    }

    public static UpdateAgencyDTO GetUpdateAgencyDTO()
    {
        return Mapper.Map<Agency, UpdateAgencyDTO>(GetAgencies().First());
    }

    public static AgencySummaryDTO GetUpdatedAgencySummary()
    {
        return Mapper.Map<Agency, AgencySummaryDTO>(GetAgencies().First());
    }

    public static IEnumerable<AgencyContactDTO> GetEmptyAgencyContacts()
    {
        return new List<AgencyContactDTO>();
    }

    public static IEnumerable<AgencyContactDTO> GetAgencyContacts()
    {
        return Mapper.Map<IEnumerable<AgencyContact>, IEnumerable<AgencyContactDTO>>(
            GetAgencies().First().AgencyContacts);
    }

    public static AgencyContactDTO GetAgencyContact()
    {
        return Mapper.Map<AgencyContact, AgencyContactDTO>(
            GetAgencies().First().AgencyContacts.First());
    }

    public static CreateAgencyContactDTO GetCreateAgencyContactDTO()
    {
        return Mapper.Map<AgencyContactDTO, CreateAgencyContactDTO>(GetAgencyContact());
    }

    public static AgencyContactDTO GetCreatedAgencyContactDTO()
    {
        return Mapper.Map<AgencyContactDTO, AgencyContactDTO>(GetAgencyContact());
    }

    public static UpdateAgencyContactDTO GetUpdateAgencyContactDTO()
    {
        return Mapper.Map<AgencyContactDTO, UpdateAgencyContactDTO>(GetAgencyContact());
    }

    public static AgencyContactDTO GetUpdatedAgencyContactDTO()
    {
        return Mapper.Map<AgencyContactDTO, AgencyContactDTO>(GetAgencyContact());
    }
}