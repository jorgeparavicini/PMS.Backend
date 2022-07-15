using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

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
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record AgencyContactDTO(
    int Id,
    [property: MaxLength(255)] string ContactName,
    [property: EmailAddress] [property: MaxLength(255)]
    string? Email,
    [property: Phone] [property: MaxLength(255)]
    string? Phone,
    [property: MaxLength(255)] string? Address,
    [property: MaxLength(255)] string? City,
    [property: MaxLength(255)] string? ZipCode,
    bool IsFrequentVendor);
