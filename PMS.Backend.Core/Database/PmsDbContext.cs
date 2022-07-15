using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Core.Database
{
    /// <summary>
    /// An EfCore context containing all tables related to the PMS project.
    /// </summary>
    public class PmsDbContext : DbContext
    {
        /// <summary>
        /// The table containing all agencies.
        /// </summary>
        /// <seealso cref="Agency"/>
        public DbSet<Agency> Agencies => Set<Agency>();
        
        /// <summary>
        /// The table containing all agencies contacts.
        /// </summary>
        /// <seealso cref="AgencyContact"/>
        public DbSet<AgencyContact> AgencyContacts => Set<AgencyContact>();

        /// <summary>
        /// The table containing all group reservations..
        /// </summary>
        /// <seealso cref="GroupReservation"/>
        public DbSet<GroupReservation> GroupReservations => Set<GroupReservation>();

        /// <summary>
        /// The table containing all reservations.
        /// </summary>
        /// <seealso cref="Reservation"/>
        public DbSet<Reservation> Reservations => Set<Reservation>();

        /// <summary>
        /// The table containing all reservation details.
        /// </summary>
        /// <seealso cref="ReservationDetail"/>
        public DbSet<ReservationDetail> ReservationDetails => Set<ReservationDetail>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PmsDbContext"/> class.
        /// </summary>
        /// <param name="options">The EF core options to be passed along.</param>
        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options)
        {
        }
    }
}
