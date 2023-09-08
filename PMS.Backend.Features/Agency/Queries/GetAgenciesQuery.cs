using MediatR;
using PMS.Backend.Features.Agency.Models;

namespace PMS.Backend.Application.Queries;

public record GetAgenciesQuery : IRequest<IQueryable<Agency>>;
