﻿using System.ComponentModel.DataAnnotations;
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
    #region Properties

    /// <summary>
    /// A unique id for the group reservation.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// An optional reference label to identify this reservation.
    /// </summary>
    [MaxLength(255)]
    public string? Reference { get; set; }

    /// <summary>
    /// The date when the reservation was made.
    /// </summary>
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// TODO: @Michael Paravicini
    /// What is the quote?
    /// </summary>
    public bool IsQuote { get; set; }

    #endregion

    #region Relations

    /// <summary>
    /// The id of the associated agency contact.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency contact are required.
    /// </remarks>
    /// <seealso cref="AgencyContact"/>
    public int AgencyContactId { get; set; }

    /// <summary>
    /// The contact who made this reservation.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the agency contact are required.
    /// </remarks>
    /// <seealso cref="AgencyContactId"/>
    [Aggregation]
    public AgencyContact AgencyContact { get; set; } = null!;

    /// <summary>
    /// A list of all reservations in this group.
    /// </summary>
    [MinLength(1)]
    [Aggregation]
    public IList<Reservation> Reservations { get; } = new List<Reservation>();

    #endregion

    // TODO: Branches
    // TODO: Quote ID: not sure what is meant by that
    // TODO: Are BookingType, Channel, Origination Enums?
}
