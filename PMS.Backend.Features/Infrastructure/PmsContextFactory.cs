using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Features.Infrastructure;

internal class PmsContextFactory(IDbContextFactory<PmsContext> pooledFactory) : IDbContextFactory<PmsContext>
{
    public PmsContext CreateDbContext()
    {
        return pooledFactory.CreateDbContext();
    }
}
