// -----------------------------------------------------------------------
// <copyright file="ReservationDetail.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace PMS.Backend.Core.Entities.Reservation;

/// <summary>
/// A details object containing the information of a passenger stay.
/// </summary>
public class ReservationDetail : Entity
{
    /// <summary>
    /// Gets or sets the date when this reservation was made.
    /// </summary>
    /// TODO: @Michael Paravicini
    /// Does this make sense as it is already in the Group Reservation
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// Gets or sets the date when the guest checks in.
    /// </summary>
    public DateOnly CheckIn { get; set; }

    /// <summary>
    /// Gets or sets the date when the guest checks out.
    /// </summary>
    public DateOnly CheckOut { get; set; }

    /// <summary>
    /// Gets or sets an optional date indicating when the reservation was closed.
    /// </summary>
    /// TODO: @Michael Paravicini
    public DateTime? FolioClosedOn { get; set; }

    /// <summary>
    /// Gets or sets the id of the parent reservation.
    /// </summary>
    /// <seealso cref="Reservation"/>
    public int ReservationId { get; set; }

    /// <summary>
    /// Gets or sets the parent reservation this detail is part of.
    /// </summary>
    /// <seealso cref="ReservationId"/>
    public required Reservation Reservation { get; set; }

    // TODO: Reservations package, pax
    // TODO: What is package?
    // TODO: Complimentary - enum?
}
