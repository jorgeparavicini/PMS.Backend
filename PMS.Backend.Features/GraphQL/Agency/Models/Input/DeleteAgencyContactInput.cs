// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using PMS.Backend.Core.Entities;
using PMS.Backend.Features.GraphQL.Agency.Mutations;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Input;

/// <summary>
///     Input for the <see cref="DeleteAgencyContactMutation"/>.
/// </summary>
public record DeleteAgencyContactInput
{
    /// <inheritdoc cref="Entity.Id"/>
    public required Guid Id { get; init; }
}
