namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

public record AgencyContactInputDTO(
    string ContactName,
    string? Email,
    string? Phone,
    string? Address,
    string? City,
    string? ZipCode,
    bool IsFrequentVendor);