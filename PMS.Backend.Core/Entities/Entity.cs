// -----------------------------------------------------------------------
// <copyright file="Entity.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;

namespace PMS.Backend.Core.Entities;

/// <summary>
/// The base class for all entities containing audit fields and helper methods.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Gets the unique Identifier of the entity.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets a timestamp used for concurrency checking.
    /// </summary>
    public byte[] RowVersion { get; init; } = Array.Empty<byte>();

    /// <summary>
    /// Gets or sets a value indicating whether a flag used to indicate that a entity is deleted and should not be used in any queries.
    /// </summary>
    public bool IsDeleted { get; set; }

    // TODO: Audit
}
