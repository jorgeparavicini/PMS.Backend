using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;
using PMS.Backend.Features.Models;

namespace PMS.Backend.Features.Agency.Commands;

public record CreateAgencyCommand(
        string LegalName,
        decimal? DefaultCommission,
        decimal? DefaultCommissionOnExtras,
        CommissionMethod CommissionMethod,
        string? EmergencyContactEmail,
        string? EmergencyContactPhone,
        IList<CreateAgencyContactInput> Contacts)
    : IRequest<CreateAgencyPayload>;

public record CreateAgencyContactInput(
    string Name,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode);