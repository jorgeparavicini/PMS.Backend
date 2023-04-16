using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;

namespace PMS.Backend.Features.Queries;

[ExtendObjectType<Query>]
public class AgencyQuery
{
    public async Task<IQueryable<AgencyDTO>> GetAgencyAsync(
        PmsDbContext context,
        IResolverContext resolverContext) => context.Agencies.ProjectTo<Agency, AgencyDTO>(resolverContext);
}
