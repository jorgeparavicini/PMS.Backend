﻿using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record UpdateAgencyDTO(
    int Id,
    string LegalName,
    decimal? DefaultCommissionRate,
    decimal? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    string? EmergencyPhone,
    string? EmergencyEmail);