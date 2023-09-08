namespace PMS.Backend.Features.Agency.Models;

public record AgencyContact(
    Guid Id,
    Guid AgencyId,
    string Name,
    string? Email,
    string? Phone,
    string? Street,
    string? City,
    string? State,
    string? Country,
    string? ZipCode)
{
    public AgencyContact() : this(
        Guid.Empty,
        Guid.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty,
        string.Empty)
    {
    }
}
