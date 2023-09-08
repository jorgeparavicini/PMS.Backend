namespace PMS.Backend.Features.Agency.Models.Input;

public record CreateAgencyInput(
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    string CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone,
    IList<CreateAgencyContactInput> Contacts);
