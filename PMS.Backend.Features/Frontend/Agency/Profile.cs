using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Core.Entities.Agency, AgencyDTO>();
        CreateMap<AgencyContact, AgencyContactDTO>();
    }
}