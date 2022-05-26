using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Core.Entities.Agency, AgencyDetailDTO>();
        CreateMap<Core.Entities.Agency, AgencySummaryDTO>();
        CreateMap<CreateAgencyDTO, Core.Entities.Agency>();
        CreateMap<UpdateAgencyDTO, Core.Entities.Agency>();
        
        CreateMap<AgencyContact, AgencyContactDTO>();
        CreateMap<CreateAgencyContactDTO, AgencyContact>();
    }
}