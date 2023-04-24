﻿// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.GraphQL.Agency.Mutations;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Input;

/// <summary>
///     Input for the <see cref="DeleteAgencyMutation"/>.
/// </summary>
public record DeleteAgencyInput
{
    /// <inheritdoc cref="Agency.Id"/>
    public required int Id { get; init; }
}