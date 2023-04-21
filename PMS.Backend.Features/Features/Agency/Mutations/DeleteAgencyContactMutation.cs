﻿// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore;
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
    /// <returns>
    ///     A <see cref="DeleteAgencyContactPayload"/> representing the result of the deletion operation.
    /// </returns>
    public async Task<DeleteAgencyContactPayload> DeleteAgencyContactAsync(
        PmsDbContext dbContext,
        DeleteAgencyContactInput input,
        [Service]
        ILogger<DeleteAgencyContactMutation> logger)
    {
        logger.ExecutingMutation(nameof(DeleteAgencyContactAsync));

        if (await dbContext.AgencyContacts.FindAsync(input.Id) is not { } entity)
        {
            throw new GraphQLException(
                ErrorBuilder.New()
                    .SetMessage($"Agency contact not found with id {input.Id}")
                    .SetCode("ENTITY_NOT_FOUND")
                    .Build());
        }

        if (entity.Agency.AgencyContacts.Count == 1)
        {
            throw new GraphQLRequestException(
                ErrorBuilder.New()
                    .SetMessage(
                        "Cannot delete the last contact for an agency. Please add a new contact before deleting this one.")
                    .SetCode("CANNOT_DELETE_LAST_CONTACT")
                    .Build());
        }

        dbContext.AgencyContacts.Remove(entity);
        await dbContext.SaveChangesAsync();

        logger.AgencyContactDeleted(entity.Id);

        return new DeleteAgencyContactPayload
        {
            ClientMutationId = string.Empty,
        };
    }
}
