// -----------------------------------------------------------------------
// <copyright file="Reservation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Detached.Annotations;

namespace PMS.Backend.Core.Entities.Reservation;

/// <summary>
/// An object that holds a list of all details regarding a reservation.
/// </summary>
public class Reservation : Entity
{
    /// <summary>
    /// Gets or sets an optional name for this reservation.
    /// </summary>
    /// <remarks>
    /// There can be multiple reservations grouped, in those cases it can be useful to give
    /// the individual reservations a specific name to identify them.
    /// </remarks>
    /// <example>
    /// Lets say the group is the family <c>Doe</c> then there could be multiple reservations
    /// and one of which could be <c>Max's Reservation</c>.
    /// </example>
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the id of the group reservation this reservation is part of.
    /// </summary>
    /// <seealso cref="GroupReservation"/>
    public int GroupReservationId { get; set; }

    /// <summary>
    /// Gets or sets the parent group reservation.
    /// </summary>
    /// <seealso cref="GroupReservationId"/>
    public required GroupReservation GroupReservation { get; set; }

    /// <summary>
    /// Gets or sets a list of all details in this reservation.
    /// </summary>
    [Composition]
    public required IList<ReservationDetail> ReservationDetails { get; set; }

    // TODO: Booking type
}
