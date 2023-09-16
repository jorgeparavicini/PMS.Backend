using JetBrains.Annotations;
using PMS.Backend.Features.Models;

namespace PMS.Backend.Features.Agency.Models;

public record Agency(
    Guid Id,
    string LegalName,
    decimal? DefaultCommission,
    decimal? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    string? EmergencyContactEmail,
    string? EmergencyContactPhone,
    IList<AgencyContact> Contacts)
{
    [UsedImplicitly]
    private Agency() : this(
        Guid.Empty,
        string.Empty,
        null,
        null,
        CommissionMethod.DeductedByAgency,
        null,
        null,
        new List<AgencyContact>())
    {
    }
}
