// -----------------------------------------------------------------------
// <copyright file="DbContextExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Detached.Mappers.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace PMS.Backend.Features.Extensions;

public static class DbContextExtensions
{
    public static async Task<TPayload> SaveAndProjectToAsync<TInput, TEntity, TPayload>(
        this DbContext dbContext,
        TInput input,
        IMapper mapper,
        CancellationToken cancellationToken = default)
        where TEntity : class
    {
        // TODO: Test whether the projection creates an additional database roundtrip if the entity already contains the data.
        var entity = await dbContext.MapAsync<TEntity>(input);
        await dbContext.SaveChangesAsync(cancellationToken);
        return mapper.Map<TPayload>(entity);
    }
}
