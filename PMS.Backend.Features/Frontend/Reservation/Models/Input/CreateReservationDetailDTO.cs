using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record CreateReservationDetailDTO(
    DateTime ReservationDate,
    DateTime CheckIn,
    DateTime CheckOut,
    DateTime? FolioClosedOn);