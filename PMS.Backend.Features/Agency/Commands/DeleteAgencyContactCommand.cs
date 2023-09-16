using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;

namespace PMS.Backend.Features.Agency.Commands;

public record DeleteAgencyContactCommand(Guid AgencyId, Guid ContactId) : IRequest<DeleteAgencyContactPayload>;
