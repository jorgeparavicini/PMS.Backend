using System.ComponentModel.DataAnnotations;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Core.Entities.Reservation;

public class GroupReservation : Entity
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    public string? Reference { get; set; }

    public DateTime ReservationDate { get; set; }

    public bool IsQuote { get; set; }

    #endregion

    #region Relations

    public int AgencyContactId { get; set; }
    public AgencyContact AgencyContact { get; set; } = null!;

    [MinLength(1)]
    public IList<Reservation> Reservations { get; } = new List<Reservation>();

    #endregion

    // TODO: Branches
    // TODO: Quote ID: not sure what is meant by that
    // TODO: Are BookingType, Channel, Origination Enums?
}