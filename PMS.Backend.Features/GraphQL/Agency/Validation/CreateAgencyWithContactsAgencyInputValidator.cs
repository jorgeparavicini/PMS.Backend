﻿// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Features.GraphQL.Agency.Validation;

/// <summary>
///     Validator for <see cref="CreateAgencyWithContactsAgencyInput"/>.
/// </summary>
public class CreateAgencyWithContactsAgencyInputValidator : AbstractValidator<CreateAgencyWithContactsAgencyInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateAgencyWithContactsAgencyInputValidator"/> class.
    /// </summary>
    public CreateAgencyWithContactsAgencyInputValidator()
    {
        RuleFor(agency => agency.LegalName).NotEmpty().MaximumLength(255);

        RuleFor(agency => agency.DefaultCommissionRate)
            .PrecisionScale(5, 4, true)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1.0m);

        RuleFor(agency => agency.DefaultCommissionOnExtras)
            .PrecisionScale(5, 4, true)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(1.0m);

        RuleFor(agency => agency.CommissionMethod.Value).IsInEnum();

        RuleFor(agency => agency.EmergencyPhone).MaximumLength(255);

        RuleFor(agency => agency.EmergencyEmail).EmailAddress().MaximumLength(255);

        RuleFor(agency => agency.AgencyContacts).NotEmpty();

        RuleForEach(agency => agency.AgencyContacts)
            .SetValidator(new CreateAgencyWithContactsAgencyContactInputValidator());
    }
}
