// -----------------------------------------------------------------------
// <copyright file="Reservation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Detached.Annotations;

namespace PMS.Backend.Core.Entities.Reservation;

/// <summary>
/// An object that holds a list of all details regarding a reservation.
/// </summary>
[Entity]
public class Reservation : Entity
{
    #region Properties

    /// <summary>
    /// An optional name for this reservation.
    /// </summary>
    /// <remarks>
    /// There can be multiple reservations grouped, in those cases it can be useful to give
    /// the individual reservations a specific name to identify them.
    /// </remarks>
    /// <example>
    /// Lets say the group is the family <c>Doe</c> then there could be multiple reservations
    /// and one of which could be <c>Max's Reservation</c>.
    /// </example>
    [MaxLength(255)]
    public string? Name { get; set; }

    #endregion

    #region Relations

    /// <summary>
    /// The id of the group reservation this reservation is part of.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the group reservation are required.
    /// </remarks>
    /// <seealso cref="GroupReservation"/>
    public int GroupReservationId { get; set; }

    /// <summary>
    /// The parent group reservation.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the group reservation are required.
    /// </remarks>
    /// <seealso cref="GroupReservationId"/>
    public GroupReservation GroupReservation { get; set; } = null!;

    /// <summary>
    /// A list of all details in this reservation.
    /// </summary>
    [MinLength(1)]
    [Composition]
    public IList<ReservationDetail> ReservationDetails { get; set; } = new List<ReservationDetail>();

    #endregion

    // TODO: Booking type
}
