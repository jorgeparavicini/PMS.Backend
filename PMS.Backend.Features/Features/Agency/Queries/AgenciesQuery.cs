// -----------------------------------------------------------------------
// <copyright file="AgenciesQuery.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Features.Agency.Models;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Queries;

/// <summary>
/// Represents a query type for Agency entities, providing support for paging, projection, sorting, and filtering.
/// </summary>
[ExtendObjectType<Query>]
public class AgenciesQuery
{
    /// <summary>
    /// Retrieves a list of <see cref="AgencyPayload"/> objects from the <see cref="PmsDbContext"/> with the applied query arguments.
    /// </summary>
    /// <param name="context">The <see cref="PmsDbContext"/> instance to query the <see cref="AgencyPayload"/> entities from.</param>
    /// <param name="mapper">The AutoMapper <see cref="IMapper"/> instance used for mapping <see cref="AgencyPayload"/> entities to <see cref="AgencyPayload"/> objects.</param>
    /// <returns>An <see cref="IQueryable"/> of <see cref="AgencyPayload"/> objects with the applied query arguments (paging, projection, sorting, and filtering).</returns>
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseSorting]
    [HotChocolate.Data.UseFiltering]
    public IQueryable<AgencyPayload> GetAgencies(PmsDbContext context, [Service] IMapper mapper)
    {
        return context.Agencies.ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
