// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactInputValidatorTests.cs" company="Vira Vira">
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
public class CreateAgencyContactInputValidatorTests
{
    private readonly CreateAgencyContactInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact Name",
            Email = "validmail@gmail.com",
            Phone = "Phone",
            Address = "Address",
            City = "City",
            ZipCode = "Zip Code",
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenAgencyIdIs0()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 0,
            ContactName = "Contact Name",
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.AgencyId);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsEmpty()
    {
        // Arrange
        CreateAgencyContactInput input = new() { AgencyId = 1, ContactName = string.Empty };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new() { AgencyId = 1, ContactName = new string('a', 256) };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact",
            Email = "invalid mail",
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact",
            Email = new string('a', 256) + "@gmail.com",
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPhoneIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact",
            Phone = new string('a', 256),
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Phone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAddressIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact",
            Address = new string('a', 256),
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Address);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCityIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact",
            City = new string('a', 256),
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.City);
    }

    [Fact]
    public void Validate_ShouldFail_WhenZipCodeIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new()
        {
            AgencyId = 1,
            ContactName = "Contact",
            ZipCode = new string('a', 256),
        };

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ZipCode);
    }
}
