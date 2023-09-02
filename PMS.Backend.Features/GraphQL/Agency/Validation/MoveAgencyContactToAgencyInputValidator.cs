// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;

namespace PMS.Backend.Features.GraphQL.Agency.Validation;

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
