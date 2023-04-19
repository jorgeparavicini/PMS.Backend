// -----------------------------------------------------------------------
// <copyright file="AgencyPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Features.Agency.Models.Payload;

/// <summary>
/// Payload for <see cref="PMS.Backend.Core.Entities.Agency.Agency"/> related queries.
/// </summary>
public record AgencyPayload
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.Id"/>
    public required int Id { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.LegalName"/>
    public required string LegalName { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionRate"/>
    public decimal? DefaultCommissionRate { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionOnExtras"/>
    public decimal? DefaultCommissionOnExtras { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.CommissionMethod"/>
    public CommissionMethod CommissionMethod { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyPhone"/>
    public string? EmergencyPhone { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyEmail"/>
    public string? EmergencyEmail { get; init; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.AgencyContacts"/>
    public required IList<AgencyContactPayload> AgencyContacts { get; init; }
}
