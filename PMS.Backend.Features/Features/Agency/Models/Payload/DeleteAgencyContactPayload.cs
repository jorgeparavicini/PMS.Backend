﻿// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Payload;

/// <summary>
///     Payload for the <see cref="DeleteAgencyContactMutation"/>.
/// </summary>
public record DeleteAgencyContactPayload
{
    /// <summary>
    ///     A unique identifier for the client performing the mutation.
    /// </summary>
    public required string ClientMutationId { get; init; }
}
