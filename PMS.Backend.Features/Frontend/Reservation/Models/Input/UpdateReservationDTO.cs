using System.ComponentModel;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Entity.Id"/>
/// </param>
/// <param name="Name">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.Name"/>
/// </param>
/// <param name="ReservationDetails">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.ReservationDetails"/>
/// </param>
public record UpdateReservationDTO(
    int Id,
    [property: DefaultValue(null)] string? Name,
    IList<UpdateReservationDetailDTO> ReservationDetails);
