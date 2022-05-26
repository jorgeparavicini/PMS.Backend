using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities;

public class AgencyContact
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    public string ContactName { get; set; } = null!;

    [MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(255)]
    public string? Phone { get; set; }

    [MaxLength(255)]
    public string? Address { get; set; }

    [MaxLength(255)]
    public string? City { get; set; }

    [MaxLength(255)]
    public string? ZipCode { get; set; }

    public bool IsFrequentVendor { get; set; }
    
    public bool Hallo { get; set; }

    #endregion

    #region Relations

    public int AgencyId { get; set; }
    public Agency Agency { get; set; } = null!;

    public IList<GroupReservation> GroupReservations { get; } = new List<GroupReservation>();

    #endregion

    // TODO: Add Country of residence, Language
}