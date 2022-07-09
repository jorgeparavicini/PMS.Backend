using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateReservationDTO(
    int Id,
    string? Name,
    IList<UpdateReservationDetailDTO> ReservationDetails);