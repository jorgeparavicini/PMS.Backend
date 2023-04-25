// -----------------------------------------------------------------------
// <copyright file="EntityTypeConfiguration.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Core.Configuration;

/// <summary>
///     The base ef core configuration for all entities.
/// </summary>
/// <typeparam name="TEntity">
///    The type of the entity to configure.
/// </typeparam>
public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity
{
    /// <inheritdoc />
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(entity => entity.Id);

        builder.Property(entity => entity.RowVersion)
            .IsRowVersion();

        builder.Property(entity => entity.IsDeleted).HasDefaultValue(false);
    }
}
