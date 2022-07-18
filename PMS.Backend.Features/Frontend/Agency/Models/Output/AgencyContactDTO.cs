namespace PMS.Backend.Features.Frontend.Agency.Models.Output;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Id"/>
/// </param>
/// <param name="ContactName">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.ContactName"/>
/// </param>
/// <param name="Email">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Email"/>
/// </param>
/// <param name="Phone">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Phone"/>
/// </param>
/// <param name="Address">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.Address"/>
/// </param>
/// <param name="City">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.City"/>
/// </param>
/// <param name="ZipCode">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.ZipCode"/>
/// </param>
/// <param name="IsFrequentVendor">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact.IsFrequentVendor"/>
/// </param>
public record AgencyContactDTO(
    int Id,
    string ContactName,
    string? Email,
    string? Phone,
    string? Address,
    string? City,
    string? ZipCode,
    bool IsFrequentVendor);
