// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Payload;

/// <summary>
///     Payload for the <see cref="DeleteAgencyMutation"/>.
/// </summary>
public record DeleteAgencyPayload
{
    /// <summary>
    ///     A unique identifier for the client performing the mutation.
    /// </summary>
    public required string ClientMutationId { get; init; }
}
