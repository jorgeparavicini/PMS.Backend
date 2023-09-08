﻿// -----------------------------------------------------------------------
// <copyright file="RequestExecutorBuilderExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Api.GraphQL.Agency.Mutations;
using PMS.Backend.Api.GraphQL.Agency.Queries;
using PMS.Backend.Api.GraphQL.Agency.Types;
using GetAgenciesQuery = PMS.Backend.Api.GraphQL.Agency.Queries.GetAgenciesQuery;

namespace PMS.Backend.Api.GraphQL.Agency.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IRequestExecutorBuilder"/> to add Agency-related functionality.
/// </summary>
public static class RequestExecutorBuilderExtensions
{
    /// <summary>
    /// Adds Agency-related types to the <see cref="IRequestExecutorBuilder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IRequestExecutorBuilder"/> instance to add the Agency-related types to.</param>
    /// <returns>The <see cref="IRequestExecutorBuilder"/> instance with the added Agency-related types.</returns>
    public static IRequestExecutorBuilder AddAgency(this IRequestExecutorBuilder builder)
    {
        return builder

            // Mutations
            .AddTypeExtension<CreateAgencyMutation>()

            // Queries
            .AddTypeExtension<GetAgenciesQuery>();
        //.AddTypeExtension<GetAgencyByIdQuery>()

        // Types
        //.AddTypeExtension<AgencyNode>()
        //.AddTypeExtension<AgencyContactNode>();
    }
}
