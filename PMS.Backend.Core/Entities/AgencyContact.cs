using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities;

public class AgencyContact
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    public string ContactName { get; set; }

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

    #endregion

    #region Relations

    public int AgencyId { get; set; }
    public Agency Agency { get; set; } = null!;

    public IList<GroupReservation> GroupReservations { get; } = new List<GroupReservation>();

    #endregion

    public AgencyContact(
        string contactName,
        bool isFrequentVendor,
        int agencyId,
        string? city = null,
        string? zipCode = null,
        string? address = null,
        string? phone = null,
        string? email = null)
    {
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        ZipCode = zipCode;
        IsFrequentVendor = isFrequentVendor;
        AgencyId = agencyId;
    }

    // TODO: Add Country of residence, Language
}