using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Models;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Queries.Handlers;

internal class GetAgencyContactsByIdsQueryHandler(PmsContext context, IMapper mapper)
    : IRequestHandler<GetAgencyContactsByIdsQuery, IReadOnlyDictionary<Guid, AgencyContact>>
{
    public async Task<IReadOnlyDictionary<Guid, AgencyContact>> Handle(
        GetAgencyContactsByIdsQuery request,
        CancellationToken cancellationToken)
    {
        return await context.AgencyContacts
            .Where(contact => request.Ids.Contains(contact.Id))
            .AsNoTracking()
            .ProjectTo<AgencyContact>(mapper.ConfigurationProvider)
            .ToDictionaryAsync(contact => contact.Id, cancellationToken: cancellationToken);
    }
}
