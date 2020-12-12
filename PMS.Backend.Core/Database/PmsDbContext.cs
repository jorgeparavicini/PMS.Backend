using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Core.Database
{
    public class PmsDbContext : DbContext
    {
        public PmsDbContext(DbContextOptions<PmsDbContext> options) : base(options)
        {
        }
        
        
    }
}
