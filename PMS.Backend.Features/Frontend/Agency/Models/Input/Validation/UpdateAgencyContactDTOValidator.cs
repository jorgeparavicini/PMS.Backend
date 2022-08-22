using FluentValidation;

namespace PMS.Backend.Features.Frontend.Agency.Models.Input.Validation;

/// <summary>
/// Validator for properties in the <see cref="UpdateAgencyContactDTO"/> class.
/// </summary>
public class UpdateAgencyContactDTOValidator : AbstractValidator<UpdateAgencyContactDTO>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateAgencyContactDTOValidator"/> class.
    /// </summary>
    public UpdateAgencyContactDTOValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.ContactName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(255);
        RuleFor(x => x.Phone).MaximumLength(255);
        RuleFor(x => x.Address).MaximumLength(255);
        RuleFor(x => x.City).MaximumLength(255);
        RuleFor(x => x.ZipCode).MaximumLength(255);
    }
}
