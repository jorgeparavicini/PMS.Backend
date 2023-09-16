using AutoMapper;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Mappings;

public class CommissionMappings : Profile
{
    public CommissionMappings()
    {
        CreateMap<Commission?, decimal?>().ConvertUsing(src => src != null ? src.Value : null);
    }
}
