namespace PMS.Backend.Application.Models.Agency.Input;

public record CreateAgencyContactInput(
    string Name,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode);
