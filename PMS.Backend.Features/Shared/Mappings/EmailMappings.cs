using AutoMapper;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Mappings;

public class EmailMappings : Profile
{
    public EmailMappings()
    {
        CreateMap<Email, string>().ConvertUsing(src => src.Value);
    }
}
