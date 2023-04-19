// -----------------------------------------------------------------------
// <copyright file="UpsertAgencyPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using HotChocolate.Types;
using PMS.Backend.Common.Models;
using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Payload;

/// <summary>
/// Payload for the <see cref="UpsertAgencyMutation"/>.
/// </summary>
public class UpsertAgencyPayload
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.Id"/>
    public int? Id { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.LegalName"/>
    public required string LegalName { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionRate"/>
    public decimal? DefaultCommissionRate { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionOnExtras"/>
    public decimal? DefaultCommissionOnExtras { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.CommissionMethod"/>
    [DefaultValue(CommissionMethod.DeductedByAgency)]
    public CommissionMethod CommissionMethod { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyPhone"/>
    public string? EmergencyPhone { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyEmail"/>
    public string? EmergencyEmail { get; init; }
}
