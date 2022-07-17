using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output.Validation;

/// <summary>
/// Validator for properties in the <see cref="AgencySummaryDTO"/> class.
/// </summary>
public class AgencySummaryDTOValidator : AbstractValidator<AgencySummaryDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AgencySummaryDTOValidator"/> class.
    /// </summary>
    public AgencySummaryDTOValidator()
    {
        RuleFor(x => x.LegalName).Length(1, 255);
        RuleFor(x => x.CommissionMethod).IsInEnum();
        RuleFor(x => x.EmergencyPhone).MaximumLength(255);
        RuleFor(x => x.EmergencyEmail).EmailAddress().MaximumLength(255);
    }
}
