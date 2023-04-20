﻿// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Features.Agency.Validation;

/// <summary>
///     Validator for <see cref="DeleteAgencyInput"/>.
/// </summary>
public class DeleteAgencyInputValidator : AbstractValidator<DeleteAgencyInput>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DeleteAgencyInputValidator"/> class.
    /// </summary>
    public DeleteAgencyInputValidator()
    {
        RuleFor(agency => agency.Id).NotEmpty();
    }
}
