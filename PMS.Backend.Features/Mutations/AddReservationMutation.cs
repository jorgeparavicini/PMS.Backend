using System.Threading.Tasks;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Features.Mutations;

[ExtendObjectType<Mutation>]
public class AddReservationMutation
{
    public async Task<GroupReservation> AddReservationAsync(PmsDbContext context, GroupReservation reservation)
    {
        await context.GroupReservations.AddAsync(reservation);
        await context.SaveChangesAsync();
        return reservation;
    }
}
