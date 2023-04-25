// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Features.GraphQL.Agency.Validation;

/// <summary>
///     Validator for <see cref="CreateAgencyWithContactsInput"/>.
/// </summary>
public class CreateAgencyWithContactsInputValidator : AbstractValidator<CreateAgencyWithContactsInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateAgencyWithContactsInputValidator"/> class.
    /// </summary>
    public CreateAgencyWithContactsInputValidator()
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

        RuleFor(agency => agency.CommissionMethod).IsInEnum();

        RuleFor(agency => agency.EmergencyPhone).MaximumLength(255);

        RuleFor(agency => agency.EmergencyEmail).EmailAddress().MaximumLength(255);
    }
}
