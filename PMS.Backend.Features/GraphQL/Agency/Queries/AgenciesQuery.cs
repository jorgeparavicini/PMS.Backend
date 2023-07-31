// -----------------------------------------------------------------------
// <copyright file="AgenciesQuery.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Agency.Queries;

/// <summary>
///     Represents a <see cref="Query"/> for <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entities,
///     providing support for paging, projection, sorting, and filtering.
/// </summary>
[ExtendObjectType<Query>]
public class AgenciesQuery
{
    /// <summary>
    ///     Retrieves a paginated list of <see cref="AgencyPayload"/> based on the specified sorting, filtering,
    ///     and projection criteria.
    /// </summary>
    /// <param name="context">The <see cref="PmsDbContext"/> instance used to query the database.</param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the
    ///     <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity to the <see cref="AgencyPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyPayload"/>s representing the agencies that match the
    ///     specified sorting, filtering, and projection criteria.
    /// </returns>
    /// <remarks>
    ///     This method supports paging, projection, sorting, and filtering. Paging allows clients to request a
    ///     specific subset of results, while projection enables them to request only specific fields. Sorting
    ///     allows clients to order the results based on one or more fields, and filtering enables them to apply
    ///     conditions to refine the results.
    /// </remarks>
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseSorting]
    [HotChocolate.Data.UseFiltering]
    public IQueryable<AgencyPayload> GetAgencies(
        PmsDbContext context,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<AgenciesQuery> logger)
    {
        logger.ExecutingQuery(nameof(AgenciesQuery));
        return context.Agencies.ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
