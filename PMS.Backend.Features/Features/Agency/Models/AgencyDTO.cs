// -----------------------------------------------------------------------
// <copyright file="AgencyDTO.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Features.Agency.Models;

/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency"/>
public record AgencyDTO
{
    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.Id"/>
    public required int Id { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.LegalName"/>
    public required string LegalName { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionRate"/>
    public decimal? DefaultCommissionRate { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionOnExtras"/>
    public decimal? DefaultCommissionOnExtras { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.CommissionMethod"/>
    public CommissionMethod CommissionMethod { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyPhone"/>
    public string? EmergencyPhone { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyEmail"/>
    public string? EmergencyEmail { get; set; }

    /// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.AgencyContacts"/>
    public required IList<AgencyContactDTO> AgencyContacts { get; set; }
}
