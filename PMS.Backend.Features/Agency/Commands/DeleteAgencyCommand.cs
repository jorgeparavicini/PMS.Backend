using MediatR;
using PMS.Backend.Features.Agency.Commands.Payloads;

namespace PMS.Backend.Features.Agency.Commands;

public record DeleteAgencyCommand(Guid Id) : IRequest<DeleteAgencyPayload>;
