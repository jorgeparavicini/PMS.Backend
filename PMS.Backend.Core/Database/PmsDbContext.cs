using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Core.Database
{
    public class PmsDbContext : DbContext
    {
        public DbSet<Agency> Agencies => Set<Agency>();
        
        public DbSet<AgencyContact> AgencyContacts => Set<AgencyContact>();

        public DbSet<GroupReservation> GroupReservations => Set<GroupReservation>();

        public DbSet<Reservation> Reservations => Set<Reservation>();

        public DbSet<ReservationDetail> ReservationDetails => Set<ReservationDetail>();

        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options)
        {
        }
    }
}
