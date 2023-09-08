namespace PMS.Backend.Features.Agency.Models;

public record Agency(
    Guid Id,
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    // TODO: Create CommissionMethodType for GraphQL and change it to the strong type instead of int.
    string CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone,
    IList<AgencyContact> Contacts)
{
    public Agency() : this(Guid.Empty, null!, null!, null!, null!, null!, null!, null!)
    {
    }
}
