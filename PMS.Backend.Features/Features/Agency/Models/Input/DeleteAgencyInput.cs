// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Input;

/// <summary>
/// The input data for a <see cref="DeleteAgencyMutation"/>.
/// </summary>
public class DeleteAgencyInput
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.Id"/>
    public required int Id { get; set; }
}
