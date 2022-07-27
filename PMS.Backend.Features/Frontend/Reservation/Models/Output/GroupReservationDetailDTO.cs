using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Entity.Id"/>
/// </param>
/// <param name="Reference">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.Reference"/>
/// </param>
/// <param name="ReservationDate">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.ReservationDate"/>
/// </param>
/// <param name="IsQuote">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.IsQuote"/>
/// </param>
/// <param name="AgencyContactId">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.AgencyContactId"/>
/// </param>
/// <param name="Reservations">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.Reservations"/>
/// </param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record GroupReservationDetailDTO(
    int Id,
    [property: MaxLength(255)] string? Reference,
    DateTime ReservationDate,
    bool IsQuote,
    int AgencyContactId,
    [property: MinLength(1)] IList<ReservationDTO> Reservations);
