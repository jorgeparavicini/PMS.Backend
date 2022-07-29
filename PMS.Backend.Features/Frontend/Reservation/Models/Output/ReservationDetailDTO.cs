﻿using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Reservation.Models.Output;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.ReservationDetail"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Entity.Id"/>
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
public record ReservationDetailDTO(
    int Id,
    DateTime ReservationDate,
    DateTime CheckIn,
    DateTime CheckOut,
    DateTime? FolioClosedOn);
