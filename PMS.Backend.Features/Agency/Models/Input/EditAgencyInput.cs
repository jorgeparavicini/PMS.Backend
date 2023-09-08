namespace PMS.Backend.Features.Agency.Models.Input;

public record EditAgencyInput(
    Guid Id,
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    string CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone);
