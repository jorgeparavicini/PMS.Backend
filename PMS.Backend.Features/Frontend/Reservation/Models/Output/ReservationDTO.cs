using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record ReservationDTO(
    int Id,
    string? Name,
    IList<ReservationDetailDTO> ReservationDetails);