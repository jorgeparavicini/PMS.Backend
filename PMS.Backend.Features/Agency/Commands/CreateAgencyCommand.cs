using MediatR;
using PMS.Backend.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Agency.Commands;

public record CreateAgencyCommand(CreateAgencyInput Input) : IRequest<Models.Agency>;
