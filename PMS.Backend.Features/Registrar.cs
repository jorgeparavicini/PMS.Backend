// -----------------------------------------------------------------------
// <copyright file="Registrar.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Features.Agency.Extensions;
using PMS.Backend.Features.Frontend.Reservation;

namespace PMS.Backend.Features;

/// <summary>
/// A class to register all graphQL endpoints and its dependencies.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Not testable.")]
public static class Registrar
{
    /// <summary>
    /// Registers all graphQL endpoints and its dependencies.
    /// </summary>
    /// <param name="services">The collection which the dependencies should be added to.</param>
    public static void AddAPI(this IServiceCollection services)
    {
        services.AddReservationAPI();
    }

    public static IRequestExecutorBuilder AddFeatureTypes(this IRequestExecutorBuilder builder)
    {
        return builder.AddAgency();
    }
}
