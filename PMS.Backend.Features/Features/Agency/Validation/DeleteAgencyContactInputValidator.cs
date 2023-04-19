// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactInputValidator.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation;
using PMS.Backend.Features.Features.Agency.Models.Input;

namespace PMS.Backend.Features.Features.Agency.Validation;

public class DeleteAgencyContactInputValidator : AbstractValidator<DeleteAgencyContactInput>
{
    public DeleteAgencyContactInputValidator()
    {
        RuleFor(agencyContact => agencyContact.Id).GreaterThan(0);
    }
}
