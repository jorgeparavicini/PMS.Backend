// -----------------------------------------------------------------------
// <copyright file="AgencyQuery.cs" company="Vira Vira">
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
///     Represents a GraphQL query for retrieving an <see cref="AgencyPayload"/> that corresponds to an
///     <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity based on specified filtering criteria.
///     This class extends the base <see cref="Query"/> type.
/// </summary>
/// <remarks>
///     The <see cref="AgencyQuery"/> class supports projection and filtering, which allows clients to request only
///     specific fields and refine the results using conditions. It includes a single method, <see cref="GetAgency"/>,
///     which retrieves the first matching <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> based on the filtering
///     criteria.
/// </remarks>
[ExtendObjectType<Query>]
public class AgencyQuery
{
    /// <summary>
    ///     Retrieves an <see cref="AgencyPayload"/> for the first <see cref="PMS.Backend.Core.Entities.Agency.Agency"/>
    ///     that matches the given filtering criteria.
    /// </summary>
    /// <param name="context">The <see cref="PmsDbContext"/> instance used to query the database.</param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the
    ///     <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity to the <see cref="AgencyPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyPayload"/> representing the first
    ///     <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> that matches the filtering criteria,
    ///     or null if no <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> matches the criteria.
    /// </returns>
    /// <remarks>
    ///     This method supports projection and filtering. Projection allows clients to request only specific fields,
    ///     while filtering enables them to apply conditions to refine the results.
    /// </remarks>
    [HotChocolate.Data.UseFirstOrDefault]
    [UseProjection]
    [HotChocolate.Data.UseFiltering]
    public IQueryable<AgencyPayload> GetAgency(
        PmsDbContext context,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<AgencyQuery> logger)
    {
        logger.ExecutingQuery(nameof(GetAgency));
        return context.Agencies.ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
