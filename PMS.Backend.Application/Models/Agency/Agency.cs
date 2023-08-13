namespace PMS.Backend.Application.Models.Agency;

public record Agency(
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    int CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone,
    IList<AgencyContact> Contacts);
