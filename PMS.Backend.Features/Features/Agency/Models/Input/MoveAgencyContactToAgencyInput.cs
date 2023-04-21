// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Input;

/// <summary>
///    Input for the <see cref="MoveAgencyContactToAgencyMutation"/>.
/// </summary>
public record MoveAgencyContactToAgencyInput
{
    /// <summary>
    ///     Gets the ID of the <see cref="AgencyContact"/> that will be moved to a different <see cref="Agency"/>.
    ///     The <see cref="AgencyContact"/> must exist in the system.
    /// </summary>
    public required int AgencyContactId { get; init; }

    /// <summary>
    ///     Gets the ID of the target <see cref="Agency"/> where the <see cref="AgencyContact"/> will be moved.
    ///     The target <see cref="Agency"/> must exist in the system.
    /// </summary>
    public required int AgencyId { get; init; }
}
