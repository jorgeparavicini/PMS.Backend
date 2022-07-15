using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.Id"/>
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
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record GroupReservationSummaryDTO(
    int Id,
    [property: MaxLength(255)] string? Reference,
    DateTime ReservationDate,
    bool IsQuote,
    int AgencyContactId);
