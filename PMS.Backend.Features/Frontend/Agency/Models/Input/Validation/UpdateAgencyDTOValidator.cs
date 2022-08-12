using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

/// <summary>
/// Validator for properties in the <see cref="UpdateAgencyDTOValidator"/> class.
/// </summary>
public class UpdateAgencyDTOValidator : AbstractValidator<UpdateAgencyDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAgencyDTOValidator"/> class.
    /// </summary>
    public UpdateAgencyDTOValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.LegalName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.EmergencyPhone).MaximumLength(255);
        RuleFor(x => x.EmergencyEmail).EmailAddress().MaximumLength(255);
        RuleFor(x => x.CommissionMethod).IsInEnum();
        RuleFor(x => x.AgencyContacts).NotNull();
    }
}
