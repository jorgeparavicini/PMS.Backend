// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using LoggerExtensions = PMS.Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions;

namespace PMS.Backend.Features.GraphQL.Agency.Mutations;

/// <summary>
///     Represents a GraphQL mutation for creating a new <see cref="AgencyContact"/> entity and returning the
///     corresponding <see cref="AgencyContactPayload"/>.
/// </summary>
[ExtendObjectType<Mutation>]
public class CreateAgencyContactMutation
{
    /// <summary>
    ///     Creates a new <see cref="AgencyContact"/> entity based on the provided input,
    ///     saves the changes to the database, and returns the corresponding <see cref="AgencyContactPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to save the new <see cref="AgencyContact"/>
    ///     entity to the database.
    /// </param>
    /// <param name="input">The input data for creating a new <see cref="AgencyContact"/> entity.</param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the <see cref="AgencyContact"/> entity to the
    ///     <see cref="AgencyContactPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyContactPayload"/> representing the newly created
    ///     <see cref="AgencyContact"/> entity.
    /// </returns>
    /// <remarks>
    ///     This method supports projection, which allows clients to request only specific fields of the
    ///     <see cref="AgencyContactPayload"/>.
    /// </remarks>
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<IQueryable<AgencyContactPayload>> CreateAgencyContactAsync(
        PmsDbContext dbContext,
        CreateAgencyContactInput input,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<CreateAgencyContactMutation> logger)
    {
        logger.ExecutingMutation(nameof(CreateAgencyContactAsync));

        // Check if the Agency exists
        if (!await dbContext.Agencies.AnyAsync(agency => agency.Id == input.AgencyId))
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage($"Agency not found with id {input.AgencyId}.")
                    .SetCode("ENTITY_NOT_FOUND")
                    .Build());
        }

        var agencyContactEntity = await dbContext.MapAsync<AgencyContact>(input);
        await dbContext.SaveChangesAsync();

        LoggerExtensions.AgencyContactCreated(logger, agencyContactEntity.Id);

        return dbContext.AgencyContacts
            .Where(agencyContact => agencyContact.Id == agencyContactEntity.Id)
            .ProjectTo<AgencyContactPayload>(mapper.ConfigurationProvider);
    }
}
