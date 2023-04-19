// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Mutations;

[ExtendObjectType<Mutation>]
public class DeleteAgencyMutation
{
    public async Task<DeleteAgencyPayload> DeleteAgencyAsync(
        PmsDbContext dbContext,
        DeleteAgencyInput input)
    {
        if (await dbContext.Agencies.Include(agency => agency.AgencyContacts)
                .FirstOrDefaultAsync(agency => agency.Id == input.Id) is { } entity)
        {
            dbContext.Agencies.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        return new DeleteAgencyPayload
        {
            ClientMutationId = string.Empty,
        };
    }
}
