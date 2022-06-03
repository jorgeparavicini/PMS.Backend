using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record CreateReservationDTO(
    string? Name,
    IList<CreateReservationDetailDTO> ReservationDetails);