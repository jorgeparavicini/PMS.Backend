using AutoMapper;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Mappings;

public class PhoneMappings : Profile
{
    public PhoneMappings()
    {
        CreateMap<Phone, string>().ConvertUsing(src => src.Value);
    }
}
