using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Queries;

namespace PMS.Backend.Features;

public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<Agency, AgencyDTO>();
    }
}
