using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Core.Database
{
    public class PmsDbContext : DbContext
    {
        public DbSet<Agency> Agencies => Set<Agency>();
        public DbSet<AgencyContact> AgencyContacts => Set<AgencyContact>();

        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options)
        {
        }
    }
}
