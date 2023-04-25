﻿// -----------------------------------------------------------------------
// <copyright file="EditAgencyContactInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Features.GraphQL.Agency.Validation;

/// <summary>
///     Validator for <see cref="EditAgencyContactInput"/>.
/// </summary>
public class EditAgencyContactInputValidator : AbstractValidator<EditAgencyContactInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EditAgencyContactInputValidator"/> class.
    /// </summary>
    public EditAgencyContactInputValidator()
    {
        RuleFor(agencyContact => agencyContact.Id)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(agencyContact => agencyContact.ContactName).NotEmpty().MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Email).EmailAddress().MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Phone).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.Address).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.City).MaximumLength(255);

        RuleFor(agencyContact => agencyContact.ZipCode).MaximumLength(255);
    }
}
