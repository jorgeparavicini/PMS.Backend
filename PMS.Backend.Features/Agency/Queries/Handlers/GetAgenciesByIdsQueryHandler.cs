using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Queries.Handlers;

internal class GetAgenciesByIdsQueryHandler(PmsContext context, IMapper mapper)
    : IRequestHandler<GetAgenciesByIdsQuery, IReadOnlyDictionary<Guid, Models.Agency>>
{
    public async Task<IReadOnlyDictionary<Guid, Models.Agency>> Handle(
        GetAgenciesByIdsQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Agencies
            .Where(agency => request.Ids.Contains(agency.Id))
            .AsNoTracking()
            .ProjectTo<Models.Agency>(mapper.ConfigurationProvider)
            .ToDictionaryAsync(agency => agency.Id, cancellationToken: cancellationToken);
    }
}
