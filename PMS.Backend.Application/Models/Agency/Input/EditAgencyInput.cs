namespace PMS.Backend.Application.Models.Agency.Input;

public record EditAgencyInput(
    Guid Id,
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    int CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone);
