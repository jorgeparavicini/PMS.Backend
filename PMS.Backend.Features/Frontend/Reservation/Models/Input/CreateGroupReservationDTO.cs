using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record CreateGroupReservationDTO(
    string? Reference, 
    DateTime ReservationDate,
    bool IsQuote,
    int AgencyContactId,
    IList<CreateReservationDTO> Reservations);