using MediatR;
using PMS.Backend.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Agency.Commands;

public record CreateAgencyContactCommand(Guid AgencyId, CreateAgencyContactInput Input) : IRequest<Models.Agency>;
