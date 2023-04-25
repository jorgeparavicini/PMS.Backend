// -----------------------------------------------------------------------
// <copyright file="ReservationDetail.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using Detached.Annotations;

namespace PMS.Backend.Core.Entities.Reservation;

/// <summary>
/// A details object containing the information of a passenger stay.
/// </summary>
[Entity]
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
    public DateTime CheckIn { get; set; }

    /// <summary>
    /// Gets or sets the date when the guest checks out.
    /// </summary>
    public DateTime CheckOut { get; set; }

    /// <summary>
    /// Gets or sets an optional date indicating when the reservation was closed.
    /// </summary>
    /// TODO: @Michael Paravicini
    /// Maybe it makes sense to add a deleted property to all entities?
    /// This seems kind of arbitrary.
    public DateTime? FolioClosedOn { get; set; }

    /// <summary>
    /// Gets or sets the id of the parent reservation.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the reservation are required.
    /// </remarks>
    /// <seealso cref="Reservation"/>
    public int ReservationId { get; set; }

    /// <summary>
    /// Gets or sets the parent reservation this detail is part of.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the reservation are required.
    /// </remarks>
    /// <seealso cref="ReservationId"/>
    public Reservation Reservation { get; set; } = null!;

    // TODO: Reservations package, pax
    // TODO: What is package?
    // TODO: Complimentary - enum?
}
