using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Infrastructure;
using HotChocolate.Data;
using PMS.Backend.Features.Agency.Entities;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Queries.Handlers;

internal class GetAgenciesQueryHandler(PmsContext context, IMapper mapper)
    : IRequestHandler<GetAgenciesQuery, IQueryable<Models.Agency>>
{
    public Task<IQueryable<Models.Agency>> Handle(GetAgenciesQuery request, CancellationToken cancellationToken)
    {
        AgencyContact contact = new("name",
            "direct@gmail.com",
            "+41 76 762 22 22",
            "street",
            "city",
            "state",
            "country",
            "zipCode");
        Entities.Agency agency = new("legalName",
            0.2m,
            0.1m,
            1,
            "email@gmail.com",
            "+41 76 762 22 22",
            new List<AgencyContact>()
            {
                contact,
            });
        var model = mapper.Map<Models.Agency>(agency);
        IQueryable<Models.Agency> query = context.Agencies
            .ProjectTo<Entities.Agency, Models.Agency>(request.ResolverContext)
            .AsNoTracking();

        return Task.FromResult(query);
    }
}
