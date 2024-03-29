﻿// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
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
///     Represents a GraphQL mutation for deleting an existing  <see cref="PMS.Backend.Core.Entities.Agency.Agency"/>
///     entity and returning a <see cref="DeleteAgencyPayload"/>.
/// </summary>
/// <remarks>
///     The <see cref="DeleteAgencyMutation"/> class includes a single method, <see cref="DeleteAgencyAsync"/>, which
///     deletes an existing  <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity
///     based on the provided input and saves the changes to the database.
/// </remarks>
[ExtendObjectType<Mutation>]
public class DeleteAgencyMutation
{
    /// <summary>
    ///     Deletes an existing <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity based on the provided
    ///     input, saves the changes to the database, and returns a <see cref="DeleteAgencyPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to delete the
    ///     <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity from the database.
    /// </param>
    /// <param name="input">
    ///     The input data for deleting the  <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entity.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    ///     A <see cref="DeleteAgencyPayload"/> representing the result of the deletion operation.
    /// </returns>
    public async Task<DeleteAgencyPayload> DeleteAgencyAsync(
        PmsDbContext dbContext,
        DeleteAgencyInput input,
        [Service]
        ILogger<DeleteAgencyMutation> logger,
        CancellationToken cancellationToken = default)
    {
        logger.ExecutingMutation(nameof(DeleteAgencyMutation));

        if (await dbContext.FindAsync<Core.Entities.Agency.Agency>(input.Id, cancellationToken) is not { } agency)
        {
            throw new NotFoundException<Core.Entities.Agency.Agency>(input.Id);
        }

        dbContext.Agencies.Remove(agency);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.AgencyDeleted(agency.Id);

        return new DeleteAgencyPayload
        {
            ClientMutationId = string.Empty,
        };
    }
}
