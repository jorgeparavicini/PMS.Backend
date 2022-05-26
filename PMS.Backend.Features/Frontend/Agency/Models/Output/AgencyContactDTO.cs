﻿using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record AgencyContactDTO(
    int Id,
    string ContactName,
    string? Email,
    string? Phone,
    string? Address,
    string? City,
    string? ZipCode,
    bool IsFrequentVendor);