using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateReservationDetailDTO(
    DateTime ReservationDate,
    DateTime CheckIn,
    DateTime CheckOut,
    DateTime? FolioClosedOn);