// -----------------------------------------------------------------------
// <copyright file="Registrar.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using HotChocolate.Execution.Configuration;
using PMS.Backend.Api.GraphQL.Agency.Extensions;

namespace PMS.Backend.Api;

/// <summary>
/// A class to register all graphQL endpoints and its dependencies.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Not testable.")]
public static class Registrar
{
    /// <summary>
    ///     Registers all graphQL endpoints and its dependencies.
    /// </summary>
    /// <param name="builder">
    ///    The <see cref="IRequestExecutorBuilder"/> to register the graphQL endpoints and its dependencies.
    /// </param>
    /// <returns>
    ///    The <see cref="IRequestExecutorBuilder"/> with the registered graphQL endpoints and its dependencies.
    /// </returns>
    public static IRequestExecutorBuilder AddGraphQlFeatures(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddAgency();
    }
}
