using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.Id"/>
/// </param>
/// <param name="Name">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.Name"/>
/// </param>
/// <param name="ReservationDetails">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.Reservation.ReservationDetails"/>
/// </param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateReservationDTO(
    [property: Required] int Id,
    [property: MaxLength(255)] [property: DefaultValue(null)]
    string? Name,
    [property: Required] [property: MinLength(1)]
    IList<UpdateReservationDetailDTO> ReservationDetails);
