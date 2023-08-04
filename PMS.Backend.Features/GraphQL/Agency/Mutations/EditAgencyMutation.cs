// -----------------------------------------------------------------------
// <copyright file="EditAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Detached.Mappers.EntityFramework;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Agency.Mutations;

/// <summary>
///     Represents a GraphQL mutation for editing an existing <see cref="Core.Entities.Agency.Agency"/> entity
///     and returning an <see cref="AgencyPayload"/>.
/// </summary>
/// <remarks>
///     The <see cref="EditAgencyMutation"/> class includes a single method, <see cref="EditAgencyAsync"/>, which
///     updates an existing <see cref="Core.Entities.Agency.Agency"/> entity based on the provided input
///     and saves the changes to the database.
/// </remarks>
[ExtendObjectType<Mutation>]
public class EditAgencyMutation
{
    /// <summary>
    ///     Edits an existing <see cref="Core.Entities.Agency.Agency"/> entity based on the provided input, saves
    ///     the changes to the database, and returns an <see cref="IQueryable"/> of <see cref="AgencyPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to edit the <see cref="Core.Entities.Agency.Agency"/> entity
    ///     in the database.
    /// </param>
    /// <param name="input">The input data for editing the <see cref="Core.Entities.Agency.Agency"/> entity.</param>
    /// <param name="mapper">The AutoMapper <see cref="IMapper"/> instance
    ///     used to map the <see cref="Core.Entities.Agency.Agency"/> entity to the <see cref="AgencyPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyPayload"/> representing
    ///     the updated <see cref="Core.Entities.Agency.Agency"/> entity.
    /// </returns>
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<IQueryable<AgencyPayload>> EditAgencyAsync(
        PmsDbContext dbContext,
        EditAgencyInput input,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<EditAgencyMutation> logger,
        CancellationToken cancellationToken = default)
    {
        logger.ExecutingMutation(nameof(EditAgencyMutation));

        if (await dbContext.FindAsync<Core.Entities.Agency.Agency>(input.Id, cancellationToken) is
            null)
        {
            throw new NotFoundException<Core.Entities.Agency.Agency>(input.Id);
        }

        var agencyEntity = await dbContext.MapAsync<Core.Entities.Agency.Agency>(input);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.AgencyEdited(agencyEntity.Id);

        return dbContext.Agencies
            .Where(agency => agency.Id == agencyEntity.Id)
            .ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
