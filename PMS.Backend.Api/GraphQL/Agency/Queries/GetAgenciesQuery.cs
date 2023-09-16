﻿using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Backend.Api.Attributes;
using PMS.Backend.Api.Extensions;

namespace PMS.Backend.Api.GraphQL.Agency.Queries;

[ExtendObjectType<Query>]
public class GetAgenciesQuery
{
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseFiltering]
    [HotChocolate.Data.UseSorting]
    [UseResolverScopedMediator]
    public async Task<IQueryable<Features.Agency.Models.Agency>> GetAgenciesAsync(
        [Service]
        IMediator mediator,
        IResolverContext resolverContext,
        [Service]
        ILogger<GetAgenciesQuery> logger)
    {
        logger.ExecutingQuery(nameof(GetAgenciesAsync));
        return await mediator.Send(new Features.Agency.Queries.GetAgenciesQuery(resolverContext));
    }
}
