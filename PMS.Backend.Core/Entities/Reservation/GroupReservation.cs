// -----------------------------------------------------------------------
// <copyright file="GroupReservation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Detached.Annotations;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Core.Entities.Reservation;

/// <summary>
/// A collection of reservations made together.
/// </summary>
/// <remarks>
/// All reservations are group reservations. Simple reservations will in that case just contain
/// a single reservation in the group.
/// </remarks>
public class GroupReservation : Entity
{
    /// <summary>
    /// Gets or sets an optional reference label to identify this reservation.
    /// </summary>
    public string? Reference { get; set; }

    /// <summary>
    /// Gets or sets the date when the reservation was made.
    /// </summary>
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this reservation is a quote.
    /// </summary>
    /// TODO: @Michael Paravicini
    public bool IsQuote { get; set; }

    /// <summary>
    /// Gets or sets the id of the associated agency contact.
    /// </summary>
    /// <seealso cref="AgencyContact"/>
    public int AgencyContactId { get; set; }

    /// <summary>
    /// Gets or sets the contact who made this reservation.
    /// </summary>
    /// <seealso cref="AgencyContactId"/>
    [Aggregation]
    public required AgencyContact AgencyContact { get; set; }

    /// <summary>
    /// Gets or sets a list of all reservations in this group.
    /// </summary>
    [Composition]
    public required IList<Reservation> Reservations { get; set; }

    // TODO: Branches
    // TODO: Quote ID: not sure what is meant by that
    // TODO: Are BookingType, Channel, Origination Enums?
}
