// -----------------------------------------------------------------------
// <copyright file="RequestExecutorBuilderExtensions.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PMS.Backend.Features.Features.Agency.Mutations;
using PMS.Backend.Features.Features.Agency.Queries;

namespace PMS.Backend.Features.Features.Agency.Extensions;

public static class RequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder AddAgency(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddTypeExtension<AddAgencyMutation>()
            .AddTypeExtension<AgencyQuery>();
    }
}
