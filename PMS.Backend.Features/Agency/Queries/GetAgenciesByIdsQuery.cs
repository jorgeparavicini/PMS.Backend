using MediatR;

namespace PMS.Backend.Features.Agency.Queries;

public record GetAgenciesByIdsQuery(IReadOnlyList<Guid> Ids)
    : IRequest<IReadOnlyDictionary<Guid, Models.Agency>>;
