using AutoMapper;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.GraphQL.Reservation.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Reservation;

/// <summary>
///     Mapping profile for all reservation related models.
/// </summary>
public class ReservationProfile : Profile
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ReservationProfile"/> class.
    /// </summary>
    public ReservationProfile()
    {
        CreateMap<GroupReservation, GroupReservationPayload>();
        CreateMap<Core.Entities.Reservation.Reservation, ReservationPayload>();
        CreateMap<ReservationDetail, ReservationDetailPayload>();
    }
}
