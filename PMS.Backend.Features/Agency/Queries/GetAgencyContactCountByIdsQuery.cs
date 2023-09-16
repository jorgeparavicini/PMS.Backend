using MediatR;

namespace PMS.Backend.Features.Agency.Queries;

public record GetAgencyContactCountByIdsQuery(IReadOnlyList<Guid> Ids)
    : IRequest<IReadOnlyDictionary<Guid, int>>;
