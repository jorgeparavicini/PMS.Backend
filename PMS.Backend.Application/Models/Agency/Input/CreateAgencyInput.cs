namespace PMS.Backend.Application.Models.Agency.Input;

public record CreateAgencyInput(
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    int CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone,
    IList<CreateAgencyContactInput> Contacts);
