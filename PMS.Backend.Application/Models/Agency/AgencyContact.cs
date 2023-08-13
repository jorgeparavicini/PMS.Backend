namespace PMS.Backend.Application.Models.Agency;

public record AgencyContact(
    string Name,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode);
