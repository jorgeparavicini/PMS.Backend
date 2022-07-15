using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Input;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail.Id"/>
/// </param>
/// <param name="ReservationDate">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail.ReservationDate"/>
/// </param>
/// <param name="CheckIn">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail.CheckIn"/>
/// </param>
/// <param name="CheckOut">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail.CheckOut"/>
/// </param>
/// <param name="FolioClosedOn">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail.FolioClosedOn"/>
/// </param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateReservationDetailDTO(
    [property: Required] int Id,
    [property: Required] DateTime ReservationDate,
    [property: Required] DateTime CheckIn,
    [property: Required] DateTime CheckOut,
    [property: DefaultValue(null)] DateTime? FolioClosedOn);
