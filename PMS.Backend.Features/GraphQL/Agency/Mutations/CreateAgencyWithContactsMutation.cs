// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsMutation.cs" company="Vira Vira">
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
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using LoggerExtensions = PMS.Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions;

namespace PMS.Backend.Features.GraphQL.Agency.Mutations;

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

        if (input.AgencyContacts.IsNullOrEmpty())
        {
            throw new GraphQLException(ErrorBuilder.New()
                .SetMessage("At least one agency contact must be provided.")
                .SetCode("AGENCY_CONTACTS_REQUIRED")
                .Build());
        }

        var agencyEntity = await dbContext.MapAsync<Core.Entities.Agency.Agency>(input);
        await dbContext.SaveChangesAsync();

        LoggerExtensions.AgencyCreated(logger, agencyEntity.Id);

        return dbContext.Agencies
            .Where(agency => agency.Id == agencyEntity.Id)
            .ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
