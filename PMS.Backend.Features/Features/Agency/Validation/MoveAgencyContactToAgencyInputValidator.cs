// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Features.Agency.Validation;

/// <summary>
///     Validator for <see cref="MoveAgencyContactToAgencyInput"/>.
/// </summary>
public class MoveAgencyContactToAgencyInputValidator : AbstractValidator<MoveAgencyContactToAgencyInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="MoveAgencyContactToAgencyInputValidator"/> class.
    /// </summary>
    public MoveAgencyContactToAgencyInputValidator()
    {
        RuleFor(input => input.AgencyContactId).NotEmpty();

        RuleFor(input => input.AgencyId).NotEmpty();
    }
}
