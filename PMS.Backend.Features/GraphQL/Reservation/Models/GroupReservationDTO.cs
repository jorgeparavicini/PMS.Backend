// -----------------------------------------------------------------------
// <copyright file="GroupReservationDTO.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace PMS.Backend.Features.GraphQL.Reservation.Models;

/// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation"/>
public record GroupReservationDTO
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.Id"/>
    public required int Id { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.Reference"/>
    public string? Reference { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.ReservationDate"/>
    public required DateTime ReservationDate { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Reservation.GroupReservation.IsQuote"/>
    public bool IsQuote { get; set; }
}
