using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;
using PMS.Backend.Api.Attributes;

namespace PMS.Backend.Api.GraphQL.Agency.Queries;

[ExtendObjectType<Query>]
public class GetAgenciesQuery
{
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseSorting]
    [UseResolverScopedMediator]
    public async Task<IQueryable<Features.Agency.Models.Agency>> GetAgenciesAsync(
        [Service]
        IMediator mediator)
    {
        return await mediator.Send(new PMS.Backend.Application.Queries.GetAgenciesQuery());
    }
}
