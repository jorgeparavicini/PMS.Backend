namespace PMS.Backend.Features.Agency.Models.Input;

public record CreateAgencyContactInput(
    string Name,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode);
