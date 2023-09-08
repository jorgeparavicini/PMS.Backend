using AutoMapper;
using JetBrains.Annotations;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Mappings;

[UsedImplicitly]
public class RequiredStringMapping : Profile
{
    public RequiredStringMapping()
    {
        CreateMap<RequiredString, string>().ConvertUsing(src => src.Value);
    }
}
