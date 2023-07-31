// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
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
///     Represents a GraphQL mutation for deleting an existing <see cref="AgencyContact"/> entity and returning a
///     <see cref="DeleteAgencyContactPayload"/>.
/// </summary>
/// <remarks>
///     The <see cref="DeleteAgencyContactMutation"/> class includes a single method,
///     <see cref="DeleteAgencyContactAsync"/>, which deletes an existing <see cref="AgencyContact"/> entity
///     based on the provided input and saves the changes to the database.
/// </remarks>
[ExtendObjectType<Mutation>]
public class DeleteAgencyContactMutation
{
    /// <summary>
    ///     Deletes an existing <see cref="AgencyContact"/> entity based on the provided input, saves
    ///     the changes to the database, and returns a <see cref="DeleteAgencyContactPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to delete the <see cref="AgencyContact"/>
    ///     entity from the database.
    /// </param>
    /// <param name="input">The input data for deleting the <see cref="AgencyContact"/> entity.</param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    ///     A <see cref="DeleteAgencyContactPayload"/> representing the result of the deletion operation.
    /// </returns>
    public async Task<DeleteAgencyContactPayload> DeleteAgencyContactAsync(
        PmsDbContext dbContext,
        DeleteAgencyContactInput input,
        [Service]
        ILogger<DeleteAgencyContactMutation> logger,
        CancellationToken cancellationToken = default)
    {
        logger.ExecutingMutation(nameof(DeleteAgencyContactMutation));

        if (await dbContext.FindAsync<AgencyContact>(input.Id, cancellationToken) is not { } agencyContact)
        {
            throw new NotFoundException<AgencyContact>(input.Id);
        }

        if (await dbContext.AgencyContacts.CountAsync(
                entity => entity.AgencyId == agencyContact.AgencyId,
                cancellationToken) <= 1)
        {
            throw new LastEntryDeletionException<Core.Entities.Agency.Agency, AgencyContact>(
                agencyContact.AgencyId,
                agencyContact.Id);
        }

        dbContext.AgencyContacts.Remove(agencyContact);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.AgencyContactDeleted(agencyContact.Id);

        return new DeleteAgencyContactPayload
        {
            ClientMutationId = string.Empty,
        };
    }
}
