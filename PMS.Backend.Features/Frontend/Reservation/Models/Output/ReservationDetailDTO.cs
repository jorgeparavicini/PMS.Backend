using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record ReservationDetailDTO(
    int Id,
    DateTime ReservationDate,
    DateTime CheckIn,
    DateTime CheckOut,
    DateTime? FolioClosedOn);