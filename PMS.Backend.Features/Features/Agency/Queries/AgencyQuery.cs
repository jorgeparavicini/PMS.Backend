// -----------------------------------------------------------------------
// <copyright file="AgencyQuery.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.Features.Agency.Models;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL;

namespace PMS.Backend.Features.Features.Agency.Queries;

[ExtendObjectType<Query>]
public class AgencyQuery
{
    [HotChocolate.Data.UseFirstOrDefault]
    [UseProjection]
    [HotChocolate.Data.UseFiltering]
    public IQueryable<AgencyPayload> GetAgency(PmsDbContext context, [Service] IMapper mapper)
    {
        return context.Agencies.ProjectTo<AgencyPayload>(mapper.ConfigurationProvider);
    }
}
