using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Core.Extensions;

/// <summary>
/// Extensions for <see cref="ChangeTracker"/>.
/// </summary>
public static class ChangeTrackerExtensions
{

    /// <summary>
    /// Sets all audit properties on a modified entry and converts hard deleted entries to soft
    /// deletions.
    /// </summary>
    /// <param name="changeTracker">The <see cref="ChangeTracker"/>.</param>
    public static void SetAuditProperties(this ChangeTracker changeTracker)
    {
        changeTracker.DetectChanges();
        var entries = changeTracker.Entries()
            .Where(x => x is { Entity: Entity, State: EntityState.Deleted });

        foreach (var entry in entries)
        {
            var entity = (Entity)entry.Entity;
            entity.IsDeleted = true;
            entry.State = EntityState.Modified;
        }
    }
}
