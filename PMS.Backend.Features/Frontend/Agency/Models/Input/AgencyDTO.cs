using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

public record AgencyDTO(
    string LegalName,
    decimal? DefaultCommissionRate,
    decimal? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    string? EmergencyPhone,
    string? EmergencyEmail,
    IReadOnlyList<Output.AgencyContactDTO> AgencyContacts)
{
    public string LegalName = LegalName;
    public decimal? DefaultCommissionRate = DefaultCommissionRate;
    public decimal? DefaultCommissionOnExtras = DefaultCommissionOnExtras;
    public CommissionMethod CommissionMethod = CommissionMethod;
    public string? EmergencyPhone = EmergencyPhone;
    public string? EmergencyEmail = EmergencyEmail;
    public IReadOnlyList<Output.AgencyContactDTO> AgencyContacts = AgencyContacts;
}