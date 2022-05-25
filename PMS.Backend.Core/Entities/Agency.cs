using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Core.Entities;

public class Agency
{
    #region Properties

    public int Id { get; set; }

    [MaxLength(255)]
    public string LegalName { get; set; }

    [Precision(18,2)]
    public decimal? DefaultCommissionRate { get; set; }

    [Precision(18, 2)]
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
        decimal? defaultCommissionRate = null,
        decimal? defaultCommissionOnExtras = null,
        string? emergencyPhone = null,
        string? emergencyEmail = null)
    {
        LegalName = legalName;
        DefaultCommissionRate = defaultCommissionRate;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyEmail = emergencyEmail;
        EmergencyPhone = emergencyPhone;
    }

    // TODO: Add Company, Association, Default Channel, Default Origination
}