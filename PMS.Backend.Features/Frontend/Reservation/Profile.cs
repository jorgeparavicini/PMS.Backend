using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;

namespace PMS.Backend.Features.Frontend.Reservation;

/// <summary>
/// The automapper profile for all reservation entities and models.
/// </summary>
[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public class Profile : AutoMapper.Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Profile"/> class.
    /// </summary>
    public Profile()
    {
        CreateMap<GroupReservation, GroupReservationSummaryDTO>();
        CreateMap<GroupReservation, GroupReservationDetailDTO>();
        CreateMap<Core.Entities.Reservation.Reservation, ReservationDTO>();
        CreateMap<ReservationDetail, ReservationDetailDTO>();
    }
}
