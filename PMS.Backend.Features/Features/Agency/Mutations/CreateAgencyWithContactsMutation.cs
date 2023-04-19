// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Detached.Mappers.EntityFramework;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Features.Agency.Extensions;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Mutations;

/// <summary>
///     Represents a GraphQL mutation for creating a new <see cref="Core.Entities.Agency.Agency"/> entity along with
///     its associated <see cref="AgencyContact"/>s, and returning the corresponding <see cref="AgencyPayload"/>.
/// </summary>
/// <remarks>
///     The <see cref="CreateAgencyWithContactsMutation"/> class includes a single method,
///     <see cref="CreateAgencyWithContactsAsync"/>, which creates a new <see cref="Core.Entities.Agency.Agency"/>
///     entity along with its associated <see cref="AgencyContact"/>s based on the provided input and saves the
///     changes to the database.
/// </remarks>
[ExtendObjectType<Mutation>]
public class CreateAgencyWithContactsMutation
{
    /// <summary>
    ///     Creates a new <see cref="Core.Entities.Agency.Agency"/> entity along with its associated
    ///     <see cref="AgencyContact"/>s based on the provided input,
    ///     saves the changes to the database, and returns the corresponding <see cref="AgencyPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to save the new <see cref="Core.Entities.Agency.Agency"/>
    ///     entity and its associated <see cref="AgencyContact"/>s to the database.
    /// </param>
    /// <param name="input">
    ///     The input data for creating a new <see cref="Core.Entities.Agency.Agency"/> entity
    ///     and its associated <see cref="AgencyContact"/>s.
    /// </param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the <see cref="Core.Entities.Agency.Agency"/>
    ///     entity to the <see cref="AgencyPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyPayload"/> representing the newly created
    ///     <see cref="Core.Entities.Agency.Agency"/> entity along with its associated <see cref="AgencyContact"/>s.
    /// </returns>
    /// <remarks>
    ///     This method supports projection, which allows clients to request only specific fields of the
    ///     AgencyPayload.
    /// </remarks>
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<IQueryable<AgencyPayload>> CreateAgencyWithContactsAsync(
        PmsDbContext dbContext,
        CreateAgencyWithContactsInput input,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<CreateAgencyWithContactsMutation> logger)
    {
        logger.ExecutingMutation(nameof(CreateAgencyWithContactsAsync));

        var agency = await dbContext.MapAsync<Core.Entities.Agency.Agency>(input);
        await dbContext.SaveChangesAsync();

        logger.AgencyCreated(agency.Id);

        return dbContext.Agencies.ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
