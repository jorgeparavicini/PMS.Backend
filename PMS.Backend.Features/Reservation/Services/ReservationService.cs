using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities;
using PMS.Backend.Features.Registration.Services.Contracts;

namespace PMS.Backend.Features.Registration.Services;

public class ReservationService : IReservationService
{

    private readonly PmsDbContext _dbContext;

    public ReservationService(PmsDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<Reservation?> GetReservation(int id)
    {
        return await _dbContext.Reservations.FindAsync(id);
    }
}