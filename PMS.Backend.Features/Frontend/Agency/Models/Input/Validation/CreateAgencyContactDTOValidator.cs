using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

/// <summary>
/// Validator for properties of the <see cref="CreateAgencyContactDTO"/> class.
/// </summary>
public class CreateAgencyContactDTOValidator : AbstractValidator<CreateAgencyContactDTO>
{
    /// <summary>
    /// Initializes a new <see cref="CreateAgencyContactDTOValidator"/> instance.
    /// </summary>
    public CreateAgencyContactDTOValidator()
    {
        RuleFor(x => x.ContactName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(255);
        RuleFor(x => x.Phone).MaximumLength(255);
        RuleFor(x => x.Address).MaximumLength(255);
        RuleFor(x => x.City).MaximumLength(255);
        RuleFor(x => x.ZipCode).MaximumLength(255);
    }
}
