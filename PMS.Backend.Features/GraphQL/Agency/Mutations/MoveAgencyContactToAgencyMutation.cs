// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Agency.Mutations;

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
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
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
        ILogger<MoveAgencyContactToAgencyMutation> logger,
        CancellationToken cancellationToken = default)
    {
        logger.ExecutingMutation(nameof(MoveAgencyContactToAgencyMutation));

        if (await dbContext.FindAsync<AgencyContact>(input.AgencyContactId, cancellationToken) is not { } agencyContact)
        {
            throw new NotFoundException<AgencyContact>(input.AgencyContactId);
        }

        if (agencyContact.AgencyId == input.AgencyId)
        {
            logger.AgencyContactIsAlreadyAssignedToAgency(agencyContact.Id, agencyContact.AgencyId);
        }

        if (await dbContext.Agencies.Where(entity => entity.Id == input.AgencyId)
                .Include(entity => entity.AgencyContacts)
                .FirstOrDefaultAsync(cancellationToken) is not { } agency)
        {
            throw new NotFoundException<Core.Entities.Agency.Agency>(input.AgencyId);
        }

        agency.AgencyContacts.Add(agencyContact);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.AgencyContactMovedToAgency(agencyContact.Id, agency.Id);

        return dbContext.Agencies
            .Where(entity => entity.Id == agency.Id)
            .ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
