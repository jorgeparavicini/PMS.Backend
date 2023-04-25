// -----------------------------------------------------------------------
// <copyright file="GroupReservation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
[Entity]
public class GroupReservation : Entity
{
    /// <summary>
    /// Gets or sets an optional reference label to identify this reservation.
    /// </summary>
    [MaxLength(255)]
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
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency contact are required.
    /// </remarks>
    /// <seealso cref="AgencyContact"/>
    public int AgencyContactId { get; set; }

    /// <summary>
    /// Gets or sets the contact who made this reservation.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency contact are required.
    /// </remarks>
    /// <seealso cref="AgencyContactId"/>
    [Aggregation]
    public AgencyContact AgencyContact { get; set; } = null!;

    /// <summary>
    /// Gets or sets a list of all reservations in this group.
    /// </summary>
    [MinLength(1)]
    [Composition]
    public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

    // TODO: Branches
    // TODO: Quote ID: not sure what is meant by that
    // TODO: Are BookingType, Channel, Origination Enums?
}
