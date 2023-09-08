using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Application.Queries;
using PMS.Backend.Features.Infrastructure;

namespace PMS.Backend.Features.Agency.Queries.Handlers;

internal class GetAgenciesQueryHandler(PmsContext context, IMapper mapper)
    : IRequestHandler<GetAgenciesQuery, IQueryable<Models.Agency>>
{
    public Task<IQueryable<Models.Agency>> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Models.Agency> query = context.Agencies
            .AsNoTracking()
            .ProjectTo<Models.Agency>(mapper.ConfigurationProvider);

        return Task.FromResult(query);
    }
}
