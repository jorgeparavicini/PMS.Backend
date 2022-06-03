using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateReservationDTO(
    string? Name,
    IList<UpdateReservationDetailDTO> ReservationDetails);