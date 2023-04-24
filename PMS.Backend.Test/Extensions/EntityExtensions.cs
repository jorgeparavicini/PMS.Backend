// -----------------------------------------------------------------------
// <copyright file="EntityExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Core.Entities;
using PMS.Backend.Test.Assertions;

namespace PMS.Backend.Test.Extensions;

public static class EntityExtensions
{
    public static EntityAssertions<T> Should<T>(this T entity)
        where T : Entity
    {
        return new EntityAssertions<T>(entity);
    }
}
