﻿// -----------------------------------------------------------------------
// <copyright file="AgencyPayload.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using PMS.Backend.Core.Domain.Models;
using PMS.Backend.Core.Entities;

namespace PMS.Backend.Features.GraphQL.Agency.Models.Payload;

/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency"/>
public record AgencyPayload
{
    /// <inheritdoc cref="Entity.Id"/>
    public required Guid Id { get; init; }

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
