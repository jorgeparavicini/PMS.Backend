using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

/// <summary>
/// Validator for properties in the <see cref="CreateAgencyDTO"/> class.
/// </summary>
// ReSharper disable once UnusedType.Global
public class CreateAgencyDTOValidator : AbstractValidator<CreateAgencyDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAgencyDTOValidator"/> class.
    /// </summary>
    public CreateAgencyDTOValidator()
    {
        RuleFor(x => x.LegalName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.CommissionMethod).NotNull().IsInEnum();
        RuleFor(x => x.EmergencyPhone).EmailAddress().MaximumLength(255);
        RuleFor(x => x.EmergencyEmail).MaximumLength(255);
        RuleFor(x => x.AgencyContacts).NotEmpty();
    }
}
