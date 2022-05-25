﻿using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output;

public record AgencyDTO(
    int Id,
    string LegalName,
    decimal? DefaultCommissionRate,
    decimal? DefaultCommissionOnExtras,
    CommissionMethod CommissionMethod,
    string? EmergencyPhone,
    string? EmergencyEmail,
    IReadOnlyList<AgencyContactDTO> AgencyContacts);