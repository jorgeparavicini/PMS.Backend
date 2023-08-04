// -----------------------------------------------------------------------
// <copyright file="EditAgencyContactInputValidatorTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using FluentValidation.TestHelper;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Validation;
using PMS.Backend.Test.Builders.Agency.Models.Input;
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
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithId(Guid.NewGuid())
            .WithContactName("Contact Name")
            .WithEmail("mail@gmail.com")
            .WithPhone("Phone")
            .WithAddress("Address")
            .WithCity("City")
            .WithZipCode("Zip Code")
            .WithIsFrequentVendor(true)
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldSucceed_WhenBasicInput()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithId(Guid.NewGuid())
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenIdIsInvalid()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Id);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsEmpty()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithContactName(string.Empty)
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithContactName(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithEmail("mail")
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithEmail(new string('a', 256) + "@gmail.com")
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenZipCodeIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithZipCode(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ZipCode);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCityIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithCity(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.City);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAddressIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithAddress(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Address);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPhoneIsTooLong()
    {
        // Arrange
        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithPhone(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<EditAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Phone);
    }
}
