using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

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
public record UpdateAgencyContactDTO(
    [property: Required] int Id,
    [property: Required] [property: MaxLength(255)]
    string ContactName,
    [property: EmailAddress] [property: MaxLength(255)] [property: DefaultValue(null)]
    string? Email,
    [property: Phone] [property: MaxLength(255)] [property: DefaultValue(null)]
    string? Phone,
    [property: MaxLength(255)] [property: DefaultValue(null)]
    string? Address,
    [property: MaxLength(255)] [property: DefaultValue(null)]
    string? City,
    [property: MaxLength(255)] [property: DefaultValue(null)]
    string? ZipCode,
    [property: DefaultValue(false)] bool IsFrequentVendor = false);
