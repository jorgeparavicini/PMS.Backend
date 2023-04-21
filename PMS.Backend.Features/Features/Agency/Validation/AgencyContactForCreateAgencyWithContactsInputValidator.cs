// -----------------------------------------------------------------------
// <copyright file="AgencyContactForCreateAgencyWithContactsInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Features.Agency.Validation;

/// <summary>
///   Validator for <see cref="AgencyContactForCreateAgencyWithContactsInput"/>.
/// </summary>
public class AgencyContactForCreateAgencyWithContactsInputValidator
    : AbstractValidator<AgencyContactForCreateAgencyWithContactsInput>
{
    /// <summary>
    ///    Initializes a new instance of the <see cref="AgencyContactForCreateAgencyWithContactsInputValidator"/> class.
    /// </summary>
    public AgencyContactForCreateAgencyWithContactsInputValidator()
    {
        RuleFor(agencyContact => agencyContact.ContactName).NotEmpty().MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Email).EmailAddress().MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Phone).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Address).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.City).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.ZipCode).MaximumLength(255);
    }
}
