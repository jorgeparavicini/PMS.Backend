﻿namespace PMS.Backend.Features.Frontend.Agency.Models.Output;

public record AgencyContactDTO(
    int Id,
    string ContactName,
    string? Email,
    string? Phone,
    string? Address,
    string? City,
    string? ZipCode,
    bool IsFrequentVendor);