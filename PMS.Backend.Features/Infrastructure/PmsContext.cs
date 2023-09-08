using MediatR;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Agency.Entities;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Shared;

namespace PMS.Backend.Features.Infrastructure;

internal class PmsContext(
        DbContextOptions<PmsContext> options,
        IMediator mediator)
    : DbContext(options)
{
    public DbSet<Agency.Entities.Agency> Agencies => Set<Agency.Entities.Agency>();

    public DbSet<AgencyContact> AgencyContacts => Set<AgencyContact>();

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEventsAsync(this);

        await SaveChangesAsync(cancellationToken);
        return true;
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetAuditProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .AddSoftDeleteQueryFilter()
            .Ignore<DomainEvent>()
            .ApplyConfigurationsFromAssembly(typeof(PmsContext).Assembly);
    }

    private void SetAuditProperties()
    {
        /*string userId = _currentUserService.GetCurrentUserId();

        foreach (EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreatedAuditProperties(userId);
                    break;

                case EntityState.Modified:
                    entry.Entity.SetUpdatedAuditProperties(userId);
                    break;
            }
        }*/
    }
}
