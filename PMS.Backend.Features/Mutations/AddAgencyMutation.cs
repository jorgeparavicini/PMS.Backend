using System.Threading.Tasks;
using Detached.Mappers.EntityFramework;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Frontend.Agency.Models.Input;

namespace PMS.Backend.Features.Mutations;

[ExtendObjectType<Mutation>]
public class AddAgencyMutation
{
    public async Task<Core.Entities.Agency.Agency> AddAgency(PmsDbContext context, CreateAgencyDTO input)
    {
        var agency = await context.MapAsync<Core.Entities.Agency.Agency>(input);
        await context.SaveChangesAsync();
        return agency;
    }
}
