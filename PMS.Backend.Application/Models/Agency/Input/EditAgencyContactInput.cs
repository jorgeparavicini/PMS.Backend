namespace PMS.Backend.Application.Models.Agency.Input;

public record EditAgencyContactInput(
    Guid Id,
    string Name,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode);
