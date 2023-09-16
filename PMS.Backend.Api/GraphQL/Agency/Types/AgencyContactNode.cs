using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Types;
using PMS.Backend.Features.Agency.Models;

namespace PMS.Backend.Api.GraphQL.Agency.Types;

[ExtendObjectType<AgencyContact>]
public class AgencyContactNode
{
    public async Task<Features.Agency.Models.Agency> GetAgencyAsync(
        [Parent]
        AgencyContact agencyContact,
        IAgencyByIdDataLoader agencyById,
        CancellationToken cancellationToken)
        => await agencyById.LoadAsync(agencyContact.AgencyId, cancellationToken);
}
