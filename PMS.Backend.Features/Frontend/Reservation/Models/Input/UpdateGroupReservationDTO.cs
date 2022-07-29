using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
    [property: Required] int Id,
    [property: MaxLength(255)] [property: DefaultValue(null)]
    string? Reference,
    [property: Required] DateTime ReservationDate,
    [property: DefaultValue(false)] bool IsQuote,
    [property: Required] int AgencyContactId,
    [property: MinLength(1)] [property: Required]
    IList<UpdateReservationDTO> Reservations);
