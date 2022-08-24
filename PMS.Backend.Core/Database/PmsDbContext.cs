using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using PMS.Backend.Core.Attributes;
using PMS.Backend.Core.Entities;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Core.Extensions;

namespace PMS.Backend.Core.Database;

/// <summary>
/// An EfCore context containing all tables related to the PMS project.
/// </summary>
public class PmsDbContext : DbContext
{
    /// <summary>
    /// The table containing all agencies.
    /// </summary>
    /// <seealso cref="Agency"/>
    [BusinessObject(Agency.BusinessObjectName)]
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
    [BusinessObject(GroupReservation.BusinessObjectName)]
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

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Expression<Func<Entity, bool>> filterExpr = e => !e.IsDeleted;
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isDeletedProperty = entityType.FindProperty(nameof(Entity.IsDeleted));
            if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(),
                    parameter,
                    filterExpr.Body);
                var filter = Expression.Lambda(body, parameter);
                entityType.SetQueryFilter(filter);
            }
        }
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <inheritdoc />
    public override int SaveChanges()
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChanges();
    }

    /// <inheritdoc />
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ChangeTracker.SetAuditProperties();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// <para>
    /// Saves all changes made in this context to the database and hard deletes entities
    /// marked for deletion.
    /// </para>
    /// <para>
    /// This method will automatically call <see cref="ChangeTracker.DetectChanges()"/> to discover
    /// any changes to entity instances before saving to the underlying database.
    /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
    /// </para>
    /// <para>
    /// Entity Framework Core does not support multiple parallel
    /// operations being run on the same DbContext instance.
    /// This includes both parallel execution of async queries and any explicit
    /// concurrent use from multiple threads. Therefore, always await async calls immediately,
    /// or use separate DbContext instances for operations that execute in parallel.
    /// </para>
    /// </summary>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database.
    /// </returns>
    public Task<int> ForceSaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// <para>
    /// Saves all changes made in this context to the database and hard deletes entities
    /// marked for deletion.
    /// </para>
    /// <para>
    /// This method will automatically call <see cref="ChangeTracker.DetectChanges()"/> to discover
    /// any changes to entity instances before saving to the underlying database.
    /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
    /// </para>
    /// <para>
    /// Entity Framework Core does not support multiple parallel
    /// operations being run on the same DbContext instance.
    /// This includes both parallel execution of async queries and any explicit
    /// concurrent use from multiple threads. Therefore, always await async calls immediately,
    /// or use separate DbContext instances for operations that execute in parallel.
    /// </para>
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">
    /// Indicates whether <see cref="ChangeTracker.AcceptAllChanges" /> is called after the changes
    /// have been sent successfully to the database.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database.
    /// </returns>
    public Task<int> ForceSaveChangesAsync(
        bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// <para>
    /// Saves all changes made in this context to the database and hard deletes entities
    /// marked for deletion.
    /// </para>
    /// <para>
    /// This method will automatically call <see cref="ChangeTracker.DetectChanges()"/> to discover
    /// any changes to entity instances before saving to the underlying database.
    /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
    /// </para>
    /// <para>
    /// Entity Framework Core does not support multiple parallel
    /// operations being run on the same DbContext instance.
    /// This includes both parallel execution of async queries and any explicit
    /// concurrent use from multiple threads. Therefore, always await async calls immediately,
    /// or use separate DbContext instances for operations that execute in parallel.
    /// </para>
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database.
    /// </returns>
    public int ForceSaveChanges()
    {
        return base.SaveChanges();
    }

    /// <summary>
    /// <para>
    /// Saves all changes made in this context to the database and hard deletes entities
    /// marked for deletion.
    /// </para>
    /// <para>
    /// This method will automatically call <see cref="ChangeTracker.DetectChanges()"/> to discover
    /// any changes to entity instances before saving to the underlying database.
    /// This can be disabled via <see cref="ChangeTracker.AutoDetectChangesEnabled" />.
    /// </para>
    /// <para>
    /// Entity Framework Core does not support multiple parallel
    /// operations being run on the same DbContext instance.
    /// This includes both parallel execution of async queries and any explicit
    /// concurrent use from multiple threads. Therefore, always await async calls immediately,
    /// or use separate DbContext instances for operations that execute in parallel.
    /// </para>
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">
    /// Indicates whether <see cref="ChangeTracker.AcceptAllChanges" /> is called after the changes
    /// have been sent successfully to the database.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous save operation.
    /// The task result contains the number of state entries written to the database.
    /// </returns>
    public int ForceSaveChanges(bool acceptAllChangesOnSuccess)
    {
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
}
