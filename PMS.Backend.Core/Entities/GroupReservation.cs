using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities;

public class GroupReservation
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
    
    public IList<Reservation> Reservations { get; } = new List<Reservation>();

    #endregion

    public GroupReservation(string? reference, DateTime reservationDate, bool isQuote)
    {
        Reference = reference;
        ReservationDate = reservationDate;
        IsQuote = isQuote;
    }
    
    // TODO: Branches
    // TODO: Quote ID: not sure what is meant by that
    // TODO: Are BookingType, Channel, Origination Enums?
}