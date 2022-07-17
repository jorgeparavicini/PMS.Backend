using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

/// <summary>
/// Validator for properties in the <see cref="CreateAgencyContactDTO"/> class.
/// </summary>
// ReSharper disable once UnusedType.Global
public class CreateAgencyContactDTOValidator : AbstractValidator<CreateAgencyContactDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAgencyContactDTOValidator"/> class.
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
