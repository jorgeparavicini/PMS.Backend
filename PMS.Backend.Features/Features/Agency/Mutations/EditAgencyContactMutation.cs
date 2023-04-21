// -----------------------------------------------------------------------
// <copyright file="EditAgencyContactMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Detached.Mappers.EntityFramework;
using HotChocolate;
using HotChocolate.Data;
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
///     Represents a GraphQL mutation for editing an existing <see cref="AgencyContact"/> entity and returning an
///     <see cref="AgencyContactPayload"/>.
/// </summary>
/// <remarks>
///     The <see cref="EditAgencyContactMutation"/> class includes a single method,
///     <see cref="EditAgencyContactAsync"/>, which updates an existing <see cref="AgencyContact"/> entity
///     based on the provided input and saves the changes to the database.
/// </remarks>
[ExtendObjectType<Mutation>]
public class EditAgencyContactMutation
{
    /// <summary>
    ///     Edits an existing <see cref="AgencyContact"/> entity based on the provided input, saves
    ///     the changes to the database, and returns an <see cref="IQueryable"/> of <see cref="AgencyContactPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to edit the <see cref="AgencyContact"/> entity in the database.
    /// </param>
    /// <param name="input">The input data for editing the <see cref="AgencyContact"/> entity.</param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the <see cref="AgencyContact"/> entity
    ///     to the <see cref="AgencyContactPayload"/>.
    /// </param>
    /// <param name="logger">The <see cref="ILogger"/> instance used for logging.</param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="AgencyContactPayload"/> representing the updated
    ///     <see cref="AgencyContact"/> entity.
    /// </returns>
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<IQueryable<AgencyContactPayload>> EditAgencyContactAsync(
        PmsDbContext dbContext,
        EditAgencyContactInput input,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<EditAgencyContactMutation> logger)
    {
        logger.ExecutingMutation(nameof(EditAgencyContactAsync));

        var agencyContact = await dbContext.MapAsync<AgencyContact>(input);
        await dbContext.SaveChangesAsync();

        logger.AgencyContactEdited(agencyContact.Id);

        return dbContext.AgencyContacts.ProjectTo<AgencyContactPayload>(mapper.ConfigurationProvider);
    }
}
