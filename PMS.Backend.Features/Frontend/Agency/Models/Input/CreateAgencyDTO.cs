using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using PMS.Backend.Common.Models;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input;

/// <summary>
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency"/>
/// </summary>
/// <param name="LegalName">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.LegalName"/>
/// </param>
/// <param name="DefaultCommissionRate">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionRate"/>
/// </param>
/// <param name="DefaultCommissionOnExtras">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.DefaultCommissionOnExtras"/>
/// </param>
/// <param name="CommissionMethod">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.CommissionMethod"/>
/// </param>
/// <param name="EmergencyPhone">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyPhone"/>
/// </param>
/// <param name="EmergencyEmail">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.EmergencyEmail"/>
/// </param>
/// <param name="AgencyContacts">
/// <inheritdoc cref="PMS.Backend.Core.Entities.Agency.Agency.AgencyContacts"/>
/// </param>
[SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Global")]
public record CreateAgencyDTO(
    [property: Required] [property: MaxLength(255)]
    string LegalName,
    [property: DefaultValue(null)] decimal? DefaultCommissionRate,
    [property: DefaultValue(null)] decimal? DefaultCommissionOnExtras,
    [property: Phone] [property: MaxLength(255)] [property: DefaultValue(null)]
    string? EmergencyPhone,
    [property: EmailAddress] [property: MaxLength(255)] [property: DefaultValue(null)]
    string? EmergencyEmail,
    [property: Required] [property: MinLength(1)]
    IReadOnlyList<CreateAgencyContactDTO> AgencyContacts,
    [property: DefaultValue(CommissionMethod.DeductedByProvider)]
    CommissionMethod CommissionMethod = CommissionMethod.DeductedByProvider);
