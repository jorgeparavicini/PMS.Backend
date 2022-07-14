﻿using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.Id"/>
/// </param>
/// <param name="LegalName">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.LegalName"/>
/// </param>
/// <param name="DefaultCommissionRate">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionRate"/>
/// </param>
/// <param name="DefaultCommissionOnExtras">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionOnExtras"/>
/// </param>
/// <param name="CommissionMethod">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.CommissionMethod"/>
/// </param>
/// <param name="EmergencyPhone">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyPhone"/>
/// </param>
/// <param name="EmergencyEmail">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyEmail"/>
/// </param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record AgencySummaryDTO(
    int Id,
    string LegalName,
    decimal? DefaultCommissionRate,
    decimal? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    string? EmergencyPhone,
    string? EmergencyEmail);