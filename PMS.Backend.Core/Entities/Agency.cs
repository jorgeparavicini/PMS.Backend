using System.ComponentModel.DataAnnotations;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Core.Entities;

public class Agency
{
    public int Id { get; set; }
    
    [MaxLength(255)]
    public string LegalName { get; set; }
    
    public decimal DefaultCommissionRate { get; set; }
    
    public decimal DefaultCommissionOnExtras { get; set; }
    
    public CommissionMethod CommissionMethod { get; set; }
    
    [MaxLength(255)]
    public string? EmergencyPhone { get; set; }
    
    [MaxLength(255)]
    public string? EmergencyEmail { get; set; }

    public Agency(
        string legalName,
        decimal defaultCommissionRate,
        decimal defaultCommissionOnExtras,
        CommissionMethod commissionMethod,
        string? emergencyPhone,
        string? emergencyEmail)
    {
        LegalName = legalName;
        DefaultCommissionRate = defaultCommissionRate;
        DefaultCommissionOnExtras = defaultCommissionOnExtras;
        CommissionMethod = commissionMethod;
        EmergencyPhone = emergencyPhone;
        EmergencyEmail = emergencyEmail;
    }
}