// -----------------------------------------------------------------------
// <copyright file="EditAgencyContactInputValidatorTests.cs" company="Vira Vira">
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
public class EditAgencyContactInputValidatorTests
{
    private readonly EditAgencyContactInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            Email = "mail@gmail.com",
            ZipCode = "Zip Code",
            City = "City",
            Address = "Address",
            Phone = "Phone",
            IsFrequentVendor = true,
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_ShouldFail_WhenIdIsInvalid(int id)
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = id,
            ContactName = "Contact Name",
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Id);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsEmpty()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = string.Empty,
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            Email = "invalid email",
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            Email = new string('a', 256) + "@gmail.com",
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenZipCodeIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            ZipCode = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ZipCode);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCityIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            City = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.City);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAddressIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            Address = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Address);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPhoneIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new()
        {
            Id = 1,
            ContactName = "Contact Name",
            Phone = new string('a', 256),
        };

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Phone);
    }
}
