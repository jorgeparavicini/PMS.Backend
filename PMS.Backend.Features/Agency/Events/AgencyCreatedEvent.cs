using PMS.Backend.Features.Shared;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Events;

internal record AgencyCreatedEvent(
    Guid AgencyId,
    string LegalName,
    Commission? DefaultCommission,
    Commission? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    ContactDetails EmergencyContact) : DomainEvent;
