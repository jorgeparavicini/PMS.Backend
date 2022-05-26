using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record CreateAgencyContactDTO(
    string ContactName,
    string? Email,
    string? Phone,
    string? Address,
    string? City,
    string? ZipCode,
    bool IsFrequentVendor);