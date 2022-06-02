using AutoMapper;
using PMS.Backend.Common.Models;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Test.FeaturesTests.FrontendTests.AgencyTests.Mock;

public static class AgencyMockData
{
    private static MapperConfiguration Config { get; } = new(cfg =>
    {
        cfg.CreateMap<AgencyDetailDTO, AgencySummaryDTO>();
        cfg.CreateMap<AgencyDetailDTO, CreateAgencyDTO>();
        cfg.CreateMap<AgencyDetailDTO, UpdateAgencyDTO>();
        cfg.CreateMap<AgencyContactDTO, CreateAgencyContactDTO>();
        cfg.CreateMap<AgencyContactDTO, UpdateAgencyContactDTO>();
    });

    private static IMapper Mapper { get; } = Config.CreateMapper();
    
    private static List<AgencyDetailDTO> GetAgencyDetails()
    {
        return new List<AgencyDetailDTO>
        {
            new(
                1, 
                "Agency1", 
                decimal.One, 
                decimal.One,
                CommissionMethod.DeductedByAgency, 
                "EmergencyPhone1", 
                "EmergencyEmail1", 
                new List<AgencyContactDTO>
                {
                    new(1,
                        "Contact1",
                        "Email1",
                        "Phone1",
                        "Address1",
                        "City1",
                        "Zip1",
                        true),
                    new(2,
                        "Contact2",
                        "Email2",
                        "Phone2",
                        "Address2",
                        "City2",
                        "Zip2",
                        true)
                }),
            new(
                2, 
                "Agency2", 
                decimal.Zero, 
                decimal.Zero,
                CommissionMethod.DeductedByProvider, 
                "EmergencyPhone2", 
                "EmergencyEmail2", 
                new List<AgencyContactDTO>())
        };
    }

    public static IEnumerable<AgencySummaryDTO> GetEmptyAgencies()
    {
        return new List<AgencySummaryDTO>();
    }

    public static IEnumerable<AgencySummaryDTO> GetAgencySummaries()
    {
        return Mapper.Map<List<AgencyDetailDTO>, List<AgencySummaryDTO>>(GetAgencyDetails());
    }

    public static AgencyDetailDTO GetAgencyDetail()
    {
        return GetAgencyDetails().First();
    }

    public static CreateAgencyDTO GetCreateAgencyDTO()
    {
        return Mapper.Map<AgencyDetailDTO, CreateAgencyDTO>(GetAgencyDetails().First());
    }

    public static AgencySummaryDTO GetCreatedAgencySummary()
    {
        return Mapper.Map<AgencyDetailDTO, AgencySummaryDTO>(GetAgencyDetails().First());
    }

    public static UpdateAgencyDTO GetUpdateAgencyDTO()
    {
        return Mapper.Map<AgencyDetailDTO, UpdateAgencyDTO>(GetAgencyDetails().First());
    }

    public static AgencySummaryDTO GetUpdatedAgencySummary()
    {
        return Mapper.Map<AgencyDetailDTO, AgencySummaryDTO>(GetAgencyDetails().First());
    }

    public static IEnumerable<AgencyContactDTO> GetEmptyAgencyContacts()
    {
        return new List<AgencyContactDTO>();
    }

    public static IEnumerable<AgencyContactDTO> GetAgencyContacts()
    {
        return GetAgencyDetails().First().AgencyContacts;
    }

    public static AgencyContactDTO GetAgencyContact()
    {
        return GetAgencyDetails().First().AgencyContacts.First();
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