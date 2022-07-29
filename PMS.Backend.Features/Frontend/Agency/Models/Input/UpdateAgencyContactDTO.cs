using System.ComponentModel;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.AgencyContact"/>
/// </summary>
/// <param name="Id">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Entity.Id"/>
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
public record UpdateAgencyContactDTO(
    int Id,
    string ContactName,
    [property: DefaultValue(null)] string? Email,
    [property: DefaultValue(null)] string? Phone,
    [property: DefaultValue(null)] string? Address,
    [property: DefaultValue(null)] string? City,
    [property: DefaultValue(null)] string? ZipCode,
    [property: DefaultValue(false)] bool IsFrequentVendor = false);
