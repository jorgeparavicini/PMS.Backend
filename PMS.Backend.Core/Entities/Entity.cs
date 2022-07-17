using System.ComponentModel.DataAnnotations;

namespace PMS.Backend.Core.Entities;

/// <summary>
/// The base class for all entities containing audit fields and helper methods.
/// </summary>
public class Entity
{
    /// <summary>
    /// A timestamp used for concurrency checking.
    /// </summary>
    [Timestamp]
    public byte[] TimeStamp { get; set; } = null!;

    // TODO: Audit
}
