using AutoMapper;
using JetBrains.Annotations;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Shared.Mappings;

[UsedImplicitly]
public class CommissionMethodMapping : Profile
{
    public CommissionMethodMapping()
    {
        CreateMap<CommissionMethod, Models.CommissionMethod>().ConvertUsing(src => (Models.CommissionMethod)src.Id);
    }
}
