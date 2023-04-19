// -----------------------------------------------------------------------
// <copyright file="IRequestExecutorBuilderExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Features.Agency.Mutations;
using PMS.Backend.Features.Features.Agency.Queries;

namespace PMS.Backend.Features.Features.Agency.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IRequestExecutorBuilder"/> to add Agency-related functionality.
/// </summary>
public static class IRequestExecutorBuilderExtensions
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
            .AddTypeExtension<UpsertAgencyMutation>()
            .AddTypeExtension<UpsertAgencyContactMutation>()
            .AddTypeExtension<DeleteAgencyMutation>()
            .AddTypeExtension<DeleteAgencyContactMutation>()

            // Queries
            .AddTypeExtension<AgenciesQuery>()
            .AddTypeExtension<AgencyQuery>();
    }
}
