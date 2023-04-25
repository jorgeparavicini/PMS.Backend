// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsInputValidatorTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using FluentValidation.TestHelper;
using PMS.Backend.Common.Models;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Validation;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Validation;

[UnitTest]
public class CreateAgencyWithContactsInputValidatorTests
{
    private readonly CreateAgencyWithContactsInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            EmergencyEmail = "emergency@mail.com",
            EmergencyPhone = "Emergency Phone",
            DefaultCommissionRate = 0.2m,
            DefaultCommissionOnExtras = 0.1m,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>
            {
                new()
                {
                    ContactName = "Contact Name",
                    Address = "Address",
                    City = "City",
                    ZipCode = "Zip Code",
                    Email = "mail@gmail.com",
                    Phone = "Phone",
                    IsFrequentVendor = true,
                },
            },
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsEmpty()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = string.Empty,
            CommissionMethod = CommissionMethod.DeductedByAgency,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = new string('a', 256),
            CommissionMethod = CommissionMethod.DeductedByAgency,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsNegative()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            DefaultCommissionRate = -0.1m,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsGreaterThan1()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            DefaultCommissionRate = 1.1m,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateOnExtrasIsNegative()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            DefaultCommissionOnExtras = -0.1m,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateOnExtrasIsGreaterThan1()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            DefaultCommissionOnExtras = 1.1m,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCommissionMethodIsOutOfEnum()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = (CommissionMethod)100,
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.CommissionMethod);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyPhoneIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            EmergencyPhone = new string('a', 256),
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.EmergencyPhone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsInvalid()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            EmergencyEmail = "invalid mail",
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsInput input = new()
        {
            LegalName = "Agency Name",
            CommissionMethod = CommissionMethod.DeductedByAgency,
            EmergencyEmail = new string('a', 256),
            AgencyContacts = new List<AgencyContactForCreateAgencyWithContactsInput>(),
        };

        // Act
        TestValidationResult<CreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.EmergencyEmail);
    }
}
