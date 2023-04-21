// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Features.Agency.Validation;

/// <summary>
///    Validator for the <see cref="CreateAgencyContactInput"/>.
/// </summary>
public class CreateAgencyContactInputValidator : AbstractValidator<CreateAgencyContactInput>
{
    /// <summary>
    ///    Initializes a new instance of the <see cref="CreateAgencyContactInputValidator"/> class.
    /// </summary>
    public CreateAgencyContactInputValidator()
    {
        RuleFor(agencyContact => agencyContact.AgencyId).NotEmpty();

        RuleFor(agencyContact => agencyContact.ContactName).NotEmpty().MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Email).EmailAddress().MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Phone).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Address).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.City).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.ZipCode).MaximumLength(255);
    }
}
