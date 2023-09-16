using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Backend.Api.Attributes;
using PMS.Backend.Api.Extensions;

namespace PMS.Backend.Api.GraphQL.Agency.Queries;

[ExtendObjectType<Query>]
public class GetAgencyQuery
{
    [HotChocolate.Data.UseFiltering]
    [HotChocolate.Data.UseFirstOrDefault]
    [UseResolverScopedMediator]
    public async Task<IQueryable<Features.Agency.Models.Agency>> GetAgencyAsync(
        [Service]
        IMediator mediator,
        IResolverContext resolverContext,
        [Service]
        ILogger<GetAgencyQuery> logger)
    {
        logger.ExecutingQuery(nameof(GetAgencyAsync));
        return await mediator.Send(new Features.Agency.Queries.GetAgenciesQuery(resolverContext));
    }
}
