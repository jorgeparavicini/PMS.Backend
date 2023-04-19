// -----------------------------------------------------------------------
// <copyright file="UpsertAgencyContactMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Mutations;

/// <summary>
/// Represents a mutation type for upserting <see cref="PMS.Backend.Core.Entities.Agency.AgencyContact"/> entities (inserting or updating).
/// </summary>
[ExtendObjectType<Mutation>]
public class UpsertAgencyContactMutation
{
    public async Task<UpsertAgencyContactPayload> UpsertAgencyContactAsync(
        PmsDbContext dbContext,
        UpsertAgencyContactInput input,
        [Service]
        IMapper mapper)
        => await dbContext.SaveAndProjectToAsync<UpsertAgencyContactInput, AgencyContact, UpsertAgencyContactPayload>(
            input,
            mapper);
}
