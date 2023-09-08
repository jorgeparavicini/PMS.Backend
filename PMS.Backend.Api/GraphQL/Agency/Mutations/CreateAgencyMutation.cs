// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsMutation.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;
using Microsoft.Extensions.Logging;
using PMS.Backend.Features.Agency.Commands;
using PMS.Backend.Features.Agency.Models.Input;
using LoggerExtensions = PMS.Backend.Api.Extensions.LoggerExtensions;

namespace PMS.Backend.Api.GraphQL.Agency.Mutations;

[ExtendObjectType<Mutation>]
public class CreateAgencyMutation
{
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<Features.Agency.Models.Agency> CreateAgencyAsync(
        CreateAgencyInput input,
        [Service]
        ILogger<CreateAgencyMutation> logger,
        [Service]
        IMediator mediator,
        CancellationToken cancellationToken = default)
    {
        LoggerExtensions.ExecutingMutation(logger, nameof(CreateAgencyMutation));

        return await mediator.Send(new CreateAgencyCommand(input), cancellationToken);
    }
}
