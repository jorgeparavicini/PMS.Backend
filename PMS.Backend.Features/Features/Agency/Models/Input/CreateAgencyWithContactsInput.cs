// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsInput.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using HotChocolate.Types;
using PMS.Backend.Common.Models;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Features.Agency.Mutations;

namespace PMS.Backend.Features.Features.Agency.Models.Input;

/// <summary>
///     Input for the <see cref="CreateAgencyWithContactsMutation"/>.
/// </summary>
public record CreateAgencyWithContactsInput
{
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

    /// <inheritdoc cref="Agency.AgencyContacts"/>
    public required IList<AgencyContactForCreateAgencyWithContactsInput> AgencyContacts { get; init; }
}
