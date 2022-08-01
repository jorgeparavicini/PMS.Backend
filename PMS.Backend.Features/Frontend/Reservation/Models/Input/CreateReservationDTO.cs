using System.ComponentModel;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation"/>
/// </summary>
/// <param name="Name">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.Name"/>
/// </param>
/// <param name="ReservationDetails">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.ReservationDetails"/>
/// </param>
public record CreateReservationDTO(
    [property: DefaultValue(null)] string? Name,
    IList<CreateReservationDetailDTO> ReservationDetails);
