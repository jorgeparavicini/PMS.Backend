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
    /// The name of this entity as a business object.
    /// </summary>
    /// <remarks>This is used to define the endpoint and the odata metadata.</remarks>
    public const string BusinessObjectName = "Reservations";

    #region Properties

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
    [Composition]
    public IList<Reservation> Reservations { get; set; } = new List<Reservation>();

    #endregion

    // TODO: Branches
    // TODO: Quote ID: not sure what is meant by that
    // TODO: Are BookingType, Channel, Origination Enums?
}
