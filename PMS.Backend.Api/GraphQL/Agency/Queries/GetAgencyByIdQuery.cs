using System;
using System.Threading;
using System.Threading.Tasks;
using HotChocolate.Types;
using PMS.Backend.Api.GraphQL.Agency.Types;

namespace PMS.Backend.Api.GraphQL.Agency.Queries;

/*
[ExtendObjectType<Query>]
public class GetAgencyByIdQuery
{
    public async Task<Application.Models.Agency.Agency> GetAgencyByIdAsync(
        Guid id,
        IAgencyByIdDataLoader agencyById,
        CancellationToken cancellationToken)
        => await agencyById.LoadAsync(id, cancellationToken);
}
*/