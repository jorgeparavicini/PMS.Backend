using System.ComponentModel.DataAnnotations;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Core.Entities;

public class Agency
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    public string LegalName { get; set; }

    public decimal? DefaultCommissionRate { get; set; }

    public decimal? DefaultCommissionOnExtras { get; set; }

    public CommissionMethod CommissionMethod { get; set; }

    [MaxLength(255)]
    public string? EmergencyPhone { get; set; }

    [MaxLength(255)]
    public string? EmergencyEmail { get; set; }

    #endregion

    #region Relations

    public IList<AgencyContact> AgencyContacts { get; } = new List<AgencyContact>();

    #endregion

    public Agency(
        string legalName,
        CommissionMethod commissionMethod,
        string? emergencyEmail = null,
        decimal? defaultCommissionOnExtras = null,
        decimal? defaultCommissionRate = null,
        string? emergencyPhone = null)
    {
        LegalName = legalName;
        DefaultCommissionRate = defaultCommissionRate;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyPhone = emergencyPhone;
        EmergencyEmail = emergencyEmail;
    }

    // TODO: Add Company, Association, Default Channel, Default Origination
}