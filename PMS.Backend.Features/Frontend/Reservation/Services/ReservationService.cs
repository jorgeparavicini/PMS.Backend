using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Common;

namespace PMS.Backend.Features.Frontend.Reservation.Services;

/// <inheritdoc />
public class ReservationService : Service<GroupReservation>
{

    /// <summary>
    /// Initializes a new instance of the <see cref="ReservationService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ReservationService(PmsDbContext context): base(context)
    {
    }
}
