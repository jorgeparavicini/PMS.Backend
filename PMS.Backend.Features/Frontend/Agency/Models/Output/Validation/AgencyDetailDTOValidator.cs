using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output.Validation;

/// <summary>
/// Validator for properties in the <see cref="AgencyDetailDTO"/> class.
/// </summary>
public class AgencyDetailDTOValidator: AbstractValidator<AgencyDetailDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AgencyDetailDTOValidator"/> class.
    /// </summary>
    public AgencyDetailDTOValidator()
    {
        RuleFor(x => x.LegalName).Length(1, 255);
        RuleFor(x => x.CommissionMethod).IsInEnum();
        RuleFor(x => x.EmergencyPhone).MaximumLength(255);
        RuleFor(x => x.EmergencyEmail).EmailAddress().MaximumLength(255);
    }
}
