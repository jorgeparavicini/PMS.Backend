using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Frontend.Reservation.Models.Input;
using PMS.Backend.Features.Frontend.Reservation.Models.Output;

namespace PMS.Backend.Features.Frontend.Reservation;

[ExcludeFromCodeCoverage]
public class Profile : AutoMapper.Profile
{
    public Profile()
    {
        CreateMap<GroupReservation, GroupReservationSummaryDTO>();
        CreateMap<GroupReservation, GroupReservationDetailDTO>();
        CreateMap<CreateGroupReservationDTO, GroupReservation>();
        CreateMap<UpdateGroupReservationDTO, GroupReservation>();

        CreateMap<Core.Entities.Reservation.Reservation, ReservationDTO>();
        CreateMap<CreateReservationDTO, Core.Entities.Reservation.Reservation>();
        CreateMap<UpdateReservationDTO, Core.Entities.Reservation.Reservation>();

        CreateMap<ReservationDetail, ReservationDetailDTO>();
        CreateMap<CreateReservationDetailDTO, ReservationDetail>();
        CreateMap<UpdateReservationDetailDTO, ReservationDetail>();
    }
}