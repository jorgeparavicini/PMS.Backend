using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

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
public record ReservationDTO(
    int Id,
    [property: MaxLength(255)] string? Name,
    IList<ReservationDetailDTO> ReservationDetails);
