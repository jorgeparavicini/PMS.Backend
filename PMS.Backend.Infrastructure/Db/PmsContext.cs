using System.Linq.Expressions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using PMS.Backend.Domain.Aggregates.Agency;
using PMS.Backend.Domain.Common;
using PMS.Backend.Persistence.Extensions;
using PMS.Backend.Persistence.Interfaces;

namespace PMS.Backend.Persistence.Db;

public class PmsContext(
        DbContextOptions<PmsContext> options,
        ICurrentUserService currentUserService,
        IMediator mediator)
    : DbContext(options)
{
    public DbSet<Agency> Agencies => Set<Agency>();

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
        Expression<Func<Entity, bool>> filterExpr = e => !e.IsDeleted;
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IMutableProperty? isDeletedProperty = entityType.FindProperty(nameof(Entity.IsDeleted));
            if (isDeletedProperty == null || isDeletedProperty.ClrType != typeof(bool))
            {
                continue;
            }

            ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "p");
            Expression body = ReplacingExpressionVisitor.Replace(
                filterExpr.Parameters.First(),
                parameter,
                filterExpr.Body);
            LambdaExpression filter = Expression.Lambda(body, parameter);
            entityType.SetQueryFilter(filter);
        }

        // Apply fluent configurations from all entities
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PmsContext).Assembly);
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
