// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;

namespace PMS.Backend.Features.Features.Agency.Mutations;

public class DeleteAgencyContactMutation
{
    public async Task<DeleteAgencyContactPayload> DeleteAgencyContactAsync(
        PmsDbContext dbContext,
        DeleteAgencyContactInput input)
    {
        if (await dbContext.AgencyContacts
                .FirstOrDefaultAsync(agencyContact => agencyContact.Id == input.Id) is { } entity)
        {
            dbContext.AgencyContacts.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        return new DeleteAgencyContactPayload
        {
            ClientMutationId = string.Empty,
        };
    }
}
