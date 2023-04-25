﻿// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyInputValidatorTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.TestHelper;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Validation;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Validation;

[UnitTest]
public class MoveAgencyContactToAgencyInputValidatorTests
{
    private readonly MoveAgencyContactToAgencyInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyContactId = 1,
            AgencyId = 1,
        };

        // Act
        TestValidationResult<MoveAgencyContactToAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldFail_WhenAgencyContactIdIsInvalid(int agencyContactId)
    {
        // Arrange
        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyContactId = agencyContactId,
            AgencyId = 1,
        };

        // Act
        TestValidationResult<MoveAgencyContactToAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.AgencyContactId);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldFail_WhenAgencyIdIsInvalid(int agencyId)
    {
        // Arrange
        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyContactId = 1,
            AgencyId = agencyId,
        };

        // Act
        TestValidationResult<MoveAgencyContactToAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.AgencyId);
    }
}