// -----------------------------------------------------------------------
// <copyright file="UpsertAgencyInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.Features.Agency.Models;
using PMS.Backend.Features.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Features.Agency.Validation;

/// <summary>
/// Contains the validation rules for <see cref="UpsertAgencyInput"/>.
/// </summary>
public class UpsertAgencyInputValidator : AbstractValidator<UpsertAgencyInput>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpsertAgencyInputValidator"/> class.
    /// </summary>
    public UpsertAgencyInputValidator()
    {
        RuleFor(agency => agency.Id).GreaterThan(0);

        RuleFor(agency => agency.LegalName).NotEmpty().MaximumLength(255);

        RuleFor(agency => agency.DefaultCommissionRate).PrecisionScale(5, 4, true);

        RuleFor(agency => agency.DefaultCommissionOnExtras).PrecisionScale(5, 4, true);

        RuleFor(agency => agency.CommissionMethod).IsInEnum();

        RuleFor(agency => agency.EmergencyPhone).MaximumLength(255);

        RuleFor(agency => agency.EmergencyEmail).EmailAddress().MaximumLength(255);
    }
}
