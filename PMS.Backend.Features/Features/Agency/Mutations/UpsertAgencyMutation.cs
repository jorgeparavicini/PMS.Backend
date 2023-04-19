// -----------------------------------------------------------------------
// <copyright file="UpsertAgencyMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using AutoMapper;
using HotChocolate;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Mutations;

/// <summary>
/// Represents a mutation type for upserting <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> entities (inserting or updating).
/// </summary>
[ExtendObjectType<Mutation>]
public class UpsertAgencyMutation
{
    /// <summary>
    /// Inserts a new <see cref="Core.Entities.Agency.Agency"/> entity or updates an existing one in the <see cref="PmsDbContext"/> using the provided <see cref="UpsertAgencyInput"/>, and returns the upserted <see cref="AgencyPayload"/> model.
    /// </summary>
    /// <param name="dbContext">The <see cref="PmsDbContext"/> instance used to upsert the <see cref="Core.Entities.Agency.Agency"/> entity.</param>
    /// <param name="input">The <see cref="UpsertAgencyInput"/> object containing the data for inserting or updating the <see cref="Core.Entities.Agency.Agency"/> entity.</param>
    /// <param name="mapper">The AutoMapper <see cref="IMapper"/> instance used for mapping <see cref="UpsertAgencyInput"/> objects to <see cref="Core.Entities.Agency.Agency"/> entities and <see cref="Core.Entities.Agency.Agency"/> entities to <see cref="AgencyPayload"/> models.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, with the upserted <see cref="AgencyPayload"/> model as the result.</returns>
    public async Task<AgencyPayload> UpsertAgencyAsync(
        PmsDbContext dbContext,
        UpsertAgencyInput input,
        [Service]
        IMapper mapper)
        => await dbContext.SaveAndProjectToAsync<UpsertAgencyInput, Core.Entities.Agency.Agency, AgencyPayload>(
            input,
            mapper);
}
