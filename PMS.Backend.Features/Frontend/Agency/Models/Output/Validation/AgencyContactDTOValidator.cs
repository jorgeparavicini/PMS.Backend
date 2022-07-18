using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Output.Validation;

/// <summary>
/// Validator for properties in the <see cref="AgencyContactDTO"/> class.
/// </summary>
public class AgencyContactDTOValidator : AbstractValidator<AgencyContactDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AgencyContactDTOValidator"/> class.
    /// </summary>
    public AgencyContactDTOValidator()
    {
        RuleFor(x => x.ContactName).Length(1, 255);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(255);
        RuleFor(x => x.Phone).MaximumLength(255);
        RuleFor(x => x.Address).MaximumLength(255);
        RuleFor(x => x.City).MaximumLength(255);
        RuleFor(x => x.ZipCode).MaximumLength(255);
    }
}
