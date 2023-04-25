// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Features.GraphQL.Agency.Mutations;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Payload;

/// <summary>
///     Payload for the <see cref="DeleteAgencyMutation"/>.
/// </summary>
public record DeleteAgencyPayload
{
    /// <summary>
    ///     Gets a unique identifier for the client performing the mutation.
    /// </summary>
    public required string ClientMutationId { get; init; }
}
