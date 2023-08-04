// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactInputValidatorTests.cs" company="Vira Vira">
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
public class CreateAgencyContactInputValidatorTests
{
    private readonly CreateAgencyContactInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithContactName("Contact Name")
            .WithEmail("validmail@gmail.com")
            .WithPhone("Phone")
            .WithAddress("Address")
            .WithCity("City")
            .WithZipCode("Zip Code")
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenAgencyIdIs0()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.Empty)
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.AgencyId);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsEmpty()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithContactName(string.Empty)
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithContactName(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ContactName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithEmail("invalidmail")
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithEmail(new string('a', 256) + "@gmail.com")
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Email);
    }

    [Fact]
    public void Validate_ShouldFail_WhenPhoneIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithPhone(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Phone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenAddressIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithAddress(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.Address);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCityIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithCity(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.City);
    }

    [Fact]
    public void Validate_ShouldFail_WhenZipCodeIsTooLong()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.NewGuid())
            .WithZipCode(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyContactInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agencyContact => agencyContact.ZipCode);
    }
}
