using AutoMapper;

namespace PMS.Backend.Features.Agency.Mappings;

public class AgencyMappings : Profile
{
    public AgencyMappings()
    {
        CreateMap<Entities.Agency, Models.Agency>();
    }
}
