// -----------------------------------------------------------------------
// <copyright file="DbContextExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using AutoMapper;
using Detached.Mappers.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Features.Extensions;

public static class DbContextExtensions
{
    public static async Task<TResult> SaveSingle<TInput, TEntity, TResult>(this DbContext dbContext, TInput input, IMapper mapper)
        where TEntity : class
    {
        var entity = await dbContext.MapAsync<TEntity>(input);
        await dbContext.SaveChangesAsync();
        return mapper.Map<TResult>(entity);
    }
}
