using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities.Reservation;

public class Reservation
{
    #region Properties

    public int Id { get; set; }
    
    [MaxLength(255)]
    public string? Name { get; set; }

    #endregion

    #region Relations

    public int GroupReservationId { get; set; }
    public GroupReservation GroupReservation { get; set; } = null!;

    #endregion
}