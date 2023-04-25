// -----------------------------------------------------------------------
// <copyright file="AgencyContactForCreateAgencyWithContactsInputValidatorTests.cs" company="Vira Vira">
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
public class AgencyContactForCreateAgencyWithContactsInputValidatorTests
{
    private readonly AgencyContactForCreateAgencyWithContactsInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            Address = "Address",
            City = "City",
            ZipCode = "Zip Code",
            Email = "email@gmail.com",
            Phone = "Phone",
            IsFrequentVendor = true,
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsEmpty()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new() { ContactName = string.Empty };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsTooLong()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new() { ContactName = new string('a', 256) };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            Email = "invalid email",
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            Email = new string('a', 256) + "@gmail.com",
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPhoneIsTooLong()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            Phone = new string('a', 256),
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Phone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAddressIsTooLong()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            Address = new string('a', 256),
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Address);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCityIsTooLong()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            City = new string('a', 256),
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.City);
    }

    [Fact]
    public void Validate_ShouldFail_WhenZipCodeIsTooLong()
    {
        // Arrange
        AgencyContactForCreateAgencyWithContactsInput input = new()
        {
            ContactName = "Contact Name",
            ZipCode = new string('a', 256),
        };

        // Act
        TestValidationResult<AgencyContactForCreateAgencyWithContactsInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ZipCode);
    }
}
