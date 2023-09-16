using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Models;

namespace PMS.Backend.Features.Agency.Commands;

public record EditAgencyCommand(
        Guid Id,
        string LegalName,
        decimal? DefaultCommission,
        decimal? DefaultCommissionOnExtras,
        CommissionMethod CommissionMethod,
        string? EmergencyContactEmail,
        string? EmergencyContactPhone)
    : IRequest<EditAgencyPayload>;
