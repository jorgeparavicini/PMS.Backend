using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities.Reservation;

public class Reservation : Entity
{
    #region Properties

    public int Id { get; set; }
    
    [MaxLength(255)]
    public string? Name { get; set; }

    #endregion

    #region Relations

    public int GroupReservationId { get; set; }
    public GroupReservation GroupReservation { get; set; } = null!;

    [MinLength(1)]
    public IList<ReservationDetail> ReservationDetails { get; } = new List<ReservationDetail>();

    #endregion
    
    // TODO: Booking type
}