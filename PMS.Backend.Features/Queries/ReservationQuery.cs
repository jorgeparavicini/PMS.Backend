using System.Linq;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Features.Queries;

public class ReservationQuery
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<GroupReservation> GetReservation(
        [Service(ServiceKind.Synchronized)]
        PmsDbContext dbContext)
    {
        return dbContext.GroupReservations;
    }
}
