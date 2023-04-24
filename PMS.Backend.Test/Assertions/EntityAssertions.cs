// -----------------------------------------------------------------------
// <copyright file="EntityAssertions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentAssertions;
using FluentAssertions.Primitives;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Test.Assertions;

public class EntityAssertions<TEntity> : ReferenceTypeAssertions<TEntity, EntityAssertions<TEntity>>
    where TEntity : Entity
{
    public EntityAssertions(TEntity subject)
        : base(subject)
    {
    }

    protected override string Identifier => "entity";
}
