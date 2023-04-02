using System.Threading.Tasks;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Features.Queries;

public class ReservationQuery
{
    public async Task<GroupReservation?> GetReservation(
        [Service(ServiceKind.Synchronized)] PmsDbContext dbContext,
        int id)
    {
        return await dbContext.Set<GroupReservation>().FirstOrDefaultAsync(reservation => reservation.Id == id);
    }
}
