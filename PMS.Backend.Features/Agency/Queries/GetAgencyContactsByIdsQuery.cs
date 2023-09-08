using MediatR;
using PMS.Backend.Features.Agency.Models;

namespace PMS.Backend.Features.Agency.Queries;

public record GetAgencyContactsByIdsQuery(IReadOnlyList<Guid> Ids)
    : IRequest<IReadOnlyDictionary<Guid, AgencyContact>>;
