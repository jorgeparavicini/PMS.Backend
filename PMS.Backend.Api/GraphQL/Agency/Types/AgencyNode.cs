using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Api.GraphQL.Agency.Types;

/*
[ExtendObjectType<Features.Agency.Models.Agency>]
public class AgencyNode
{
    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, Application.Models.Agency.Agency>> GetAgencyByIdAsync(
        IReadOnlyList<Guid> ids,
        PmsContext context,
        [Service]
        IMapper mapper,
        IResolverContext resolverContext,
        CancellationToken cancellationToken) =>
        await context.Agencies
            .Where(agency => ids.Contains(agency.Id))
            .Project(resolverContext)
            .ProjectTo<Application.Models.Agency.Agency>(mapper.ConfigurationProvider)
            .ToDictionaryAsync(agency => agency.Id, cancellationToken: cancellationToken);
}
*/
