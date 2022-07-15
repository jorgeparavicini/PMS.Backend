namespace PMS.Backend.Core.Entities.Reservation;

/// <summary>
/// A details object containing the information of a passenger stay.
/// </summary>
public class ReservationDetail : Entity
{
    #region Properties

    /// <summary>
    /// The unique identifier of the reservation detail.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The date when this reservation was made.
    /// </summary>
    /// TODO: @Michael Paravicini
    /// Does this make sense as it is already in the Group Reservation
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// The date when the guest checks in.
    /// </summary>
    public DateTime CheckIn { get; set; }

    /// <summary>
    /// The date when the guest checks out.
    /// </summary>
    public DateTime CheckOut { get; set; }

    /// <summary>
    /// An optional date indicating when the reservation was closed.
    /// </summary>
    /// TODO: @Michael Paravicini
    /// Maybe it makes sense to add a deleted property to all entities?
    /// This seems kind of arbitrary.
    public DateTime? FolioClosedOn { get; set; }

    #endregion

    #region Relations

    /// <summary>
    /// The id of the parent reservation.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the reservation are required.
    /// </remarks>
    /// <seealso cref="Reservation"/>
    public int ReservationId { get; set; }

    /// <summary>
    /// The parent reservation this detail is part of.
    /// </summary>
    /// <remarks>
    /// This is an EF-Core relation, hence both the Id and the reservation are required.
    /// </remarks>
    /// <seealso cref="ReservationId"/>
    public Reservation Reservation { get; set; } = null!;

    #endregion

    // TODO: Reservations package, pax
    // TODO: What is package?
    // TODO: Complimentary - enum?
}
