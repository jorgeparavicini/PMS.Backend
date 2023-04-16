// -----------------------------------------------------------------------
// <copyright file="AgencyQuery.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Features.Agency.Models;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Queries;

[ExtendObjectType<Query>]
public class AgencyQuery
{
    public IQueryable<AgencyDTO> GetAgencies(PmsDbContext context, IResolverContext resolverContext)
        => context.Agencies.ProjectTo<Core.Entities.Agency.Agency, AgencyDTO>(resolverContext);
}
