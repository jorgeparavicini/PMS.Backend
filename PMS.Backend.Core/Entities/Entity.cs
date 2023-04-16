using System;

namespace PMS.Backend.Core.Entities;

/// <summary>
/// The base class for all entities containing audit fields and helper methods.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Gets or sets the unique Identifier of the entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A timestamp used for concurrency checking.
    /// </summary>
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// A flag used to indicate that a entity is deleted and should not be used in any queries.
    /// </summary>
    public bool IsDeleted { get; set; }

    // TODO: Audit
}
