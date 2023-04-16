// -----------------------------------------------------------------------
// <copyright file="AddAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using AutoMapper;
using Detached.Mappers.EntityFramework;
using HotChocolate;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Features.Agency.Models;
using PMS.Backend.Features.Frontend.Agency.Models.Input;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Mutations;

[ExtendObjectType<Mutation>]
public class AddAgencyMutation
{
    public async Task<AgencyDTO> AddAgency(PmsDbContext dbContext, CreateAgencyDTO input, [Service] IMapper mapper)
    {
        var agency = await dbContext.MapAsync<Core.Entities.Agency.Agency>(input);
        await dbContext.SaveChangesAsync();
        return mapper.Map<AgencyDTO>(agency);
    }
}
