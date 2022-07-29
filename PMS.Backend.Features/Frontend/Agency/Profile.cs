using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.Frontend.Agency.Models.Output;

namespace PMS.Backend.Features.Frontend.Agency;

/// <summary>
/// The automapper profile for all agency entities and models.
/// </summary>
[ExcludeFromCodeCoverage]
public class Profile : AutoMapper.Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Profile"/> class.
    /// </summary>
    public Profile()
    {
        CreateMap<Core.Entities.Agency.Agency, AgencyDetailDTO>();
        CreateMap<Core.Entities.Agency.Agency, AgencySummaryDTO>();
        CreateMap<AgencyContact, AgencyContactDTO>();
    }
}
