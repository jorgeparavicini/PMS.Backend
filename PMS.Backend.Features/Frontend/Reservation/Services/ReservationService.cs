using PMS.Backend.Core.Database;
using PMS.Backend.Features.Frontend.Reservation.Services.Contracts;

namespace PMS.Backend.Features.Frontend.Reservation.Services;

public class ReservationService : IReservationService
{

    private readonly PmsDbContext _dbContext;

    public ReservationService(PmsDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<Core.Entities.Reservation?> GetReservation(int id)
    {
        return await _dbContext.Reservations.FindAsync(id);
    }
}