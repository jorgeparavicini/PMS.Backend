// -----------------------------------------------------------------------
// <copyright file="EditAgencyInputValidatorTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using FluentValidation.TestHelper;
using PMS.Backend.Core.Domain.Models;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Validation;
using PMS.Backend.Test.Builders.Agency.Models.Input;
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
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithId(Guid.NewGuid())
            .WithLegalName("Test")
            .WithDefaultCommissionRate(0.1m)
            .WithDefaultCommissionOnExtras(0.1m)
            .WithCommissionMethod(CommissionMethod.DeductedByAgency)
            .WithEmergencyPhone("123456789")
            .WithEmergencyEmail("mail@gmail.com")
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldSucceed_WhenBasicInput()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithId(Guid.NewGuid())
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenIdIsInvalid()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.Id);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsEmpty()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithLegalName(string.Empty)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsTooLong()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithLegalName(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsNegative()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithDefaultCommissionRate(-0.00001m)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsGreaterThan1()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithDefaultCommissionRate(1.0001m)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionOnExtrasIsNegative()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithDefaultCommissionOnExtras(-0.00001m)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionOnExtrasIsGreaterThan1()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithDefaultCommissionOnExtras(1.0001m)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyPhoneIsTooLong()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithEmergencyPhone(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.EmergencyPhone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsTooLong()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithEmergencyEmail(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsInvalid()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithEmergencyEmail("invalidEmail")
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCommissionMethodIsInvalid()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithCommissionMethod((CommissionMethod)100)
            .Build();

        // Act
        TestValidationResult<EditAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.CommissionMethod);
    }
}
