// -----------------------------------------------------------------------
// <copyright file="EnumerableExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Test.Extensions;

public static class EnumerableExtensions
{
    public static Mock<DbSet<T>> ToMockDbSet<T>(this IEnumerable<T> data)
        where T : Entity
    {
        Mock<DbSet<T>> mockDbSet = new();
        IQueryable<T> queryable = data.AsQueryable();

        mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
        mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

        return mockDbSet;
    }
}
