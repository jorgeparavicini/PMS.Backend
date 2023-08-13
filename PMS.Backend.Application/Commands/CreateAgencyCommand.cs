using MediatR;
using PMS.Backend.Application.Models.Agency;
using PMS.Backend.Application.Models.Agency.Input;

namespace PMS.Backend.Application.Commands;

public record CreateAgencyCommand(CreateAgencyInput Input) : IRequest<Agency>;
