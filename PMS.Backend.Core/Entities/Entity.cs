using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PMS.Backend.Core.Entities;

/// <summary>
/// The base class for all entities containing audit fields and helper methods.
/// </summary>
public class Entity
{
    /// <summary>
    /// A unique Identifier for this entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// A timestamp used for concurrency checking.
    /// </summary>
    [IgnoreDataMember]
    [Timestamp]
    public byte[] TimeStamp { get; set; } = Array.Empty<byte>();

    /// <summary>
    /// A flag used to indicate that a entity is deleted and should not be used in any queries.
    /// </summary>
    [IgnoreDataMember]
    public bool IsDeleted { get; set; } = false;

    // TODO: Audit
}
