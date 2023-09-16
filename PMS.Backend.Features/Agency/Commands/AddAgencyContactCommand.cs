using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;

namespace PMS.Backend.Features.Agency.Commands;

public record AddAgencyContactCommand(
        Guid AgencyId,
        string Name,
        string? Email,
        string? Phone,
        string? Street,
        string? City,
        string? State,
        string? Country,
        string? ZipCode)
    : IRequest<AddAgencyContactPayload>;
