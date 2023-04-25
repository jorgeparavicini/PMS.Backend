// -----------------------------------------------------------------------
// <copyright file="EditAgencyInputValidatorTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.TestHelper;
using PMS.Backend.Common.Models;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Validation;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Validation;

[UnitTest]
public class EditAgencyInputValidatorTests
{
    private readonly EditAgencyInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            DefaultCommissionRate = 0.1m,
            DefaultCommissionOnExtras = 0.1m,
            CommissionMethod = CommissionMethod.DeductedByAgency,
            EmergencyPhone = "123456789",
            EmergencyEmail = "mail@gmail.com",
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldFail_WhenIdIsInvalid(int id)
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = id,
            LegalName = "Test",
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.Id);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsEmpty()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = string.Empty,
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsTooLong()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsNegative()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            DefaultCommissionRate = -0.1m,
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsGreaterThan1()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            DefaultCommissionRate = 1.1m,
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionOnExtrasIsNegative()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            DefaultCommissionOnExtras = -0.1m,
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionOnExtrasIsGreaterThan1()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            DefaultCommissionOnExtras = 1.1m,
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyPhoneIsTooLong()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            EmergencyPhone = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.EmergencyPhone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsTooLong()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            EmergencyEmail = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsInvalid()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            EmergencyEmail = "mail",
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCommissionMethodIsInvalid()
    {
        // Arrange
        EditAgencyInput input = new()
        {
            Id = 1,
            LegalName = "Test",
            CommissionMethod = (CommissionMethod)100,
        };

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.CommissionMethod);
    }
}
