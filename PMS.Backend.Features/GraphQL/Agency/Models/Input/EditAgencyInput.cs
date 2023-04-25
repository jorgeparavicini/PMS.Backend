// -----------------------------------------------------------------------
// <copyright file="EditAgencyInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Types;
using PMS.Backend.Core.Domain.Models;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.GraphQL.Agency.Mutations;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Input;

/// <summary>
///     Input for the <see cref="EditAgencyMutation"/>.
/// </summary>
public record EditAgencyInput
{
    /// <inheritdoc cref="Agency.Id"/>
    public required int Id { get; init; }

    /// <inheritdoc cref="Agency.LegalName"/>
    public required string LegalName { get; init; }

    /// <inheritdoc cref="Agency.DefaultCommissionRate"/>
    public decimal? DefaultCommissionRate { get; init; }

    /// <inheritdoc cref="Agency.DefaultCommissionOnExtras"/>
    public decimal? DefaultCommissionOnExtras { get; init; }

    /// <inheritdoc cref="Agency.CommissionMethod"/>
    [DefaultValue(CommissionMethod.DeductedByAgency)]
    public CommissionMethod CommissionMethod { get; init; }

    /// <inheritdoc cref="Agency.EmergencyPhone"/>
    public string? EmergencyPhone { get; init; }

    /// <inheritdoc cref="Agency.EmergencyEmail"/>
    public string? EmergencyEmail { get; init; }
}
