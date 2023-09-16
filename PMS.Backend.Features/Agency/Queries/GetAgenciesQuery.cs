using HotChocolate.Resolvers;
using MediatR;

namespace PMS.Backend.Features.Agency.Queries;

public record GetAgenciesQuery(IResolverContext ResolverContext) : IRequest<IQueryable<Models.Agency>>;
