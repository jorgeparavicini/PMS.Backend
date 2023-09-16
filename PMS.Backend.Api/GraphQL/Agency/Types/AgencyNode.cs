using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Backend.Api.Extensions;
using PMS.Backend.Features.Agency.Queries;

namespace PMS.Backend.Api.GraphQL.Agency.Types;

[ExtendObjectType<Features.Agency.Models.Agency>]
public class AgencyNode
{
    public async Task<int> GetContactCountAsync(
        [Parent]
        Features.Agency.Models.Agency agency,
        IAgencyContactCountByIdDataLoader agencyContactCountById,
        CancellationToken cancellationToken)
        => await agencyContactCountById.LoadAsync(agency.Id, cancellationToken);

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, Features.Agency.Models.Agency>> GetAgencyByIdAsync(
        IReadOnlyList<Guid> ids,
        [Service]
        IMediator mediator,
        [Service]
        ILogger<AgencyNode> logger,
        CancellationToken cancellationToken)
    {
        logger.ExecutingDataLoader(nameof(GetAgencyByIdAsync));
        GetAgenciesByIdsQuery query = new(ids);
        return await mediator.Send(query, cancellationToken);
    }

    [DataLoader]
    internal static async Task<IReadOnlyDictionary<Guid, int>> GetAgencyContactCountByIdAsync(
        IReadOnlyList<Guid> ids,
        [Service]
        IMediator mediator,
        [Service]
        ILogger<AgencyNode> logger,
        CancellationToken cancellationToken)
    {
        logger.ExecutingDataLoader(nameof(GetAgencyContactCountByIdAsync));
        GetAgencyContactCountByIdsQuery query = new(ids);
        return await mediator.Send(query, cancellationToken);
    }
}
