// -----------------------------------------------------------------------
// <copyright file="EntityBuilder.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Reflection;
using AutoFixture.Kernel;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Test.Common.Builders;

public class EntityBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is not PropertyInfo propertyInfo || !typeof(Entity).IsAssignableFrom(propertyInfo.DeclaringType))
        {
            return new NoSpecimen();
        }

        return propertyInfo.Name switch
        {
            nameof(Entity.Id) => 0,
            nameof(Entity.IsDeleted) => false,
            nameof(Entity.RowVersion) => Array.Empty<byte>(),
            _ => new NoSpecimen(),
        };
    }
}
