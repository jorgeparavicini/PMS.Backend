namespace PMS.Backend.Core.Entities.Reservation;

public class ReservationDetail
{
    #region Properties

    public int Id { get; set; }
    
    public DateTime ReservationDate { get; set; }
    
    public DateTime CheckIn { get; set; }
    
    public DateTime CheckOut { get; set; }
    
    public DateTime? FolioClosedOn { get; set; }

    #endregion

    #region Relations

    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;

    #endregion

    // TODO: Reservations package, pax
    // TODO: What is package?
    // TODO: Complimentary - enum?
}