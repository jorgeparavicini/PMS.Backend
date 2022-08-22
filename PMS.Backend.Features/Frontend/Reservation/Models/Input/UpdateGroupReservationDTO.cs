using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Features.Common;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

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
public record UpdateGroupReservationDTO(
        int Id,
        [property: DefaultValue(null)] string? Reference,
        DateTime ReservationDate,
        [property: DefaultValue(false)] bool IsQuote,
        int AgencyContactId,
        IList<UpdateReservationDTO> Reservations)
    : UpdateDTO(Id);
