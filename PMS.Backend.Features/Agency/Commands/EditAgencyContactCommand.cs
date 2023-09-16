using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;

namespace PMS.Backend.Features.Agency.Commands;

public record EditAgencyContactCommand(
        Guid AgencyId,
        Guid Id,
        string Name,
        string? Email,
        string? Phone,
        string? Street,
        string? City,
        string? State,
        string? Country,
        string? ZipCode)
    : IRequest<EditAgencyContactPayload>;
