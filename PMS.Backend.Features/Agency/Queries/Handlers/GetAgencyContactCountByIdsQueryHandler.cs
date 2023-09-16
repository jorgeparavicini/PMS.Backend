using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Queries.Handlers;

internal class GetAgencyContactCountByIdsQueryHandler(PmsContext context)
    : IRequestHandler<GetAgencyContactCountByIdsQuery, IReadOnlyDictionary<Guid, int>>
{
    public async Task<IReadOnlyDictionary<Guid, int>> Handle(
        GetAgencyContactCountByIdsQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Agencies
            .Where(agency => request.Ids.Contains(agency.Id))
            .AsNoTracking()
            .Select(agency => new
            {
                agency.Id,
                agency.Contacts.Count,
            })
            .ToDictionaryAsync(
                agency => agency.Id,
                agency => agency.Count,
                cancellationToken: cancellationToken);
    }
}
