using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities;

public class AgencyContact
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    public string ContactName { get; set; }

    [MaxLength(255)]
    public string Email { get; set; }

    [MaxLength(255)]
    public string Phone { get; set; }

    [MaxLength(255)]
    public string Address { get; set; }

    [MaxLength(255)]
    public string City { get; set; }

    [MaxLength(255)]
    public string ZipCode { get; set; }

    public bool IsFrequentVendor { get; set; }

    #endregion

    #region Relations

    public int AgencyId { get; set; }
    public Agency Agency { get; set; } = null!;

    #endregion

    public AgencyContact(
        string contactName,
        string email,
        string phone,
        string address,
        string city,
        string zipCode,
        bool isFrequentVendor,
        int agencyId)
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