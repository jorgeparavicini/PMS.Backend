using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output;

public record AgencyDTO(
    int Id,
    string LegalName,
    decimal? DefaultCommissionRate,
    decimal? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    string? EmergencyPhone,
    string? EmergencyEmail,
    IReadOnlyList<AgencyContactDTO> AgencyContacts)
{
    public int Id = Id;
    public string LegalName = LegalName;
    public decimal? DefaultCommissionRate = DefaultCommissionRate;
    public decimal? DefaultCommissionOnExtras = DefaultCommissionOnExtras;
    public CommissionMethod CommissionMethod = CommissionMethod;
    public string? EmergencyPhone = EmergencyPhone;
    public string? EmergencyEmail = EmergencyEmail;
    public IReadOnlyList<AgencyContactDTO> AgencyContacts = AgencyContacts;
}