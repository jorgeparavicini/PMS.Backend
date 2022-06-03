using System.ComponentModel.DataAnnotations;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Core.Entities.Agency;

public class AgencyContact : Entity
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    [Required]
    public string ContactName { get; set; } = null!;

    [MaxLength(255)]
    [EmailAddress]
    public string? Email { get; set; }

    [MaxLength(255)]
    [Phone]
    public string? Phone { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    [MaxLength(255)]
    public string? City { get; set; }

    [MaxLength(255)]
    public string? ZipCode { get; set; }

    [Required]
    public bool IsFrequentVendor { get; set; }

    #endregion

    #region Relations

    [Required]
    public int AgencyId { get; set; }
    public Agency Agency { get; set; } = null!;

    public IList<GroupReservation> GroupReservations { get; } = new List<GroupReservation>();

    #endregion

    // TODO: Add Country of residence, Language
}