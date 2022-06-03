using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateGroupReservationDTO(
    int Id,
    string? Reference,
    DateTime ReservationDate,
    bool IsQuote,
    int AgencyContactId,
    IList<UpdateReservationDTO> Reservations);