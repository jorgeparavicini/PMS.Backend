using System;
using PMS.Backend.Domain.Common;
using PMS.Backend.Domain.ValueObjects;

namespace PMS.Backend.Domain.Events.Agency;

public record AgencyCreatedEvent(
    Guid AgencyId,
    string LegalName,
    Commission? DefaultCommission,
    Commission? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    ContactDetails EmergencyContact) : DomainEvent;
