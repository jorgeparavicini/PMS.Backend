using FluentValidation;
using PMS.Backend.Features.Agency.Models.Input;
using PMS.Backend.Features.Constants;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.Shared.ValueObjects;

namespace PMS.Backend.Features.Agency.Validation;

public class CreateAgencyInputValidator : AbstractValidator<CreateAgencyInput>
{
    public CreateAgencyInputValidator()
    {
        RuleFor(input => input.LegalName).NotEmpty().MaximumLength(StringLengths.FullName);
        RuleFor(input => input.DefaultCommission).IsCommission();
        RuleFor(input => input.DefaultCommissionOnExtras).IsCommission();
        RuleFor(input => input.CommissionMethod).IsInEnum<CreateAgencyInput, CommissionMethod>();
        RuleFor(input => input.EmergencyContactPhone).MaximumLength(StringLengths.PhoneNumber);
        RuleFor(input => input.EmergencyContactEmail).EmailAddress().MaximumLength(StringLengths.Email);
    }
}
