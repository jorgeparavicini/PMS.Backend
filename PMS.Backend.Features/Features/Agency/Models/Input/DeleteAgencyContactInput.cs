﻿// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Input;

/// <summary>
///     Input for the <see cref="DeleteAgencyContactMutation"/>.
/// </summary>
public record DeleteAgencyContactInput
{
    /// <inheritdoc cref="AgencyContact.Id"/>
    public required int Id { get; init; }
}
