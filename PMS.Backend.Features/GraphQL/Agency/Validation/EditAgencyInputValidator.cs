// -----------------------------------------------------------------------
// <copyright file="EditAgencyInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Features.GraphQL.Agency.Validation;

/// <summary>
///     Validator for <see cref="EditAgencyInput"/>.
/// </summary>
public class EditAgencyInputValidator : AbstractValidator<EditAgencyInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="EditAgencyInputValidator"/> class.
    /// </summary>
    public EditAgencyInputValidator()
    {
        RuleFor(agency => agency.Id).NotEmpty();

        RuleFor(agency => agency.LegalName).NotEmpty().MaximumLength(255);

        RuleFor(agency => agency.DefaultCommissionRate).PrecisionScale(5, 4, true);

        RuleFor(agency => agency.DefaultCommissionOnExtras).PrecisionScale(5, 4, true);

        RuleFor(agency => agency.CommissionMethod).IsInEnum();

        RuleFor(agency => agency.EmergencyPhone).MaximumLength(255);

        RuleFor(agency => agency.EmergencyEmail).EmailAddress().MaximumLength(255);
    }
}
