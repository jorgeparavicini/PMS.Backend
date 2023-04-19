// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Input;

/// <summary>
/// The input data for a <see cref="DeleteAgencyContactMutation"/>.
/// </summary>
public record DeleteAgencyContactInput
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Id"/>
    public required int Id { get; set; }
}
