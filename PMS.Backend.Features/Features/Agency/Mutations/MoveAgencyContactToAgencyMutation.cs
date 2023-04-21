// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Features.Agency.Extensions;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Mutations;

/// <summary>
///     Represents a GraphQL mutation for moving an <see cref="Core.Entities.Agency.AgencyContact"/> entity
///     from one <see cref="Core.Entities.Agency.Agency"/> to another and returning an <see cref="AgencyPayload"/>.
/// </summary>
/// <remarks>
///     The <see cref="MoveAgencyContactToAgencyMutation"/> class includes a single method,
///     <see cref="MoveAgencyContactToAgencyAsync"/>, which moves an existing
///     <see cref="Core.Entities.Agency.AgencyContact"/> entity to another <see cref="Core.Entities.Agency.Agency"/>
///     based on the provided input and saves the changes to the database.
/// </remarks>
[ExtendObjectType<Mutation>]
public class MoveAgencyContactToAgencyMutation
{
    /// <summary>
    ///     Moves an existing <see cref="Core.Entities.Agency.AgencyContact"/> entity
    ///     from one <see cref="Core.Entities.Agency.Agency"/> to another, saves the changes to the database,
    ///     and returns an <see cref="IQueryable"/> of <see cref="AgencyPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to move the <see cref="Core.Entities.Agency.AgencyContact"/>
    ///     entity in the database.
    /// </param>
    /// <param name="input">
    ///     The input data for moving the <see cref="Core.Entities.Agency.AgencyContact"/> entity.
    /// </param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the
    ///     <see cref="Core.Entities.Agency.Agency"/> entity to the <see cref="AgencyPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyPayload"/> representing the updated
    ///     <see cref="Core.Entities.Agency.Agency"/> entity.
    /// </returns>
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<IQueryable<AgencyPayload>> MoveAgencyContactToAgencyAsync(
        PmsDbContext dbContext,
        MoveAgencyContactToAgencyInput input,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<MoveAgencyContactToAgencyMutation> logger)
    {
        logger.ExecutingMutation(nameof(MoveAgencyContactToAgencyAsync));

        if (await dbContext.AgencyContacts.FindAsync(input.AgencyContactId) is not { } agencyContactEntity)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage($"Agency contact not found with id {input.AgencyContactId}")
                    .SetCode("ENTITY_NOT_FOUND")
                    .Build());
        }

        if (await dbContext.Agencies.FindAsync(input.AgencyId) is not { } agencyEntity)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage($"Agency not found with id {input.AgencyId}")
                    .SetCode("ENTITY_NOT_FOUND")
                    .Build());
        }

        agencyEntity.AgencyContacts.Add(agencyContactEntity);
        await dbContext.SaveChangesAsync();

        logger.AgencyContactMovedToAgency(agencyContactEntity.Id, agencyEntity.Id);

        return dbContext.Agencies
            .Where(agency => agency.Id == agencyEntity.Id)
            .ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
