using MediatR;
using PMS.Backend.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Agency.Commands;

public record EditAgencyCommand(EditAgencyInput Input) : IRequest<Models.Agency>;
