// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsInputValidatorTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using FluentValidation.TestHelper;
using PMS.Backend.Core.Domain.Models;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Validation;
using PMS.Backend.Test.Builders.Agency.Models.Input;
using PMS.Backend.Test.Extensions;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Validation;

[UnitTest]
public class CreateAgencyWithContactsInputValidatorTests
{
    private readonly CreateAgencyWithContactsAgencyInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenFullValidInput()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithLegalName("Agency Name")
            .WithCommissionMethod(CommissionMethod.DeductedByAgency)
            .WithEmergencyEmail("emergency@mail.com")
            .WithEmergencyPhone("Emergency Phone")
            .WithDefaultCommissionRate(0.2m)
            .WithDefaultCommissionOnExtras(0.1m)
            .AddAgencyContacts(builder => builder
                .WithContactName("Contact Name")
                .WithAddress("Address")
                .WithCity("City")
                .WithZipCode("Zip Code")
                .WithEmail("mail@gmail.com")
                .WithPhone("Phone")
                .WithIsFrequentVendor(true)
                .Build())
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldSucceed_WhenBasicValidInput()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(_ => { })
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsEmpty()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithLegalName(string.Empty)
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenLegalNameIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithLegalName(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.LegalName);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsNegative()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithDefaultCommissionRate(-0.1m)
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateIsGreaterThan1()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithDefaultCommissionRate(1.00001m)
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionRate);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateOnExtrasIsNegative()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithDefaultCommissionOnExtras(-0.0001m)
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenDefaultCommissionRateOnExtrasIsGreaterThan1()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithDefaultCommissionOnExtras(1.00001m)
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.DefaultCommissionOnExtras);
    }

    [Fact]
    public void Validate_ShouldFail_WhenCommissionMethodIsOutOfEnum()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithCommissionMethod((CommissionMethod)200)
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.CommissionMethod);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyPhoneIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithEmergencyPhone(new string('a', 256))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.EmergencyPhone);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsInvalid()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithEmergencyEmail("invalid email")
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmergencyEmailIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .WithEmergencyEmail(new string('a', 256) + "@mail.com")
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(createAgency => createAgency.EmergencyEmail);
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsEmpty()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithContactName(string.Empty))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.ContactName));
    }

    [Fact]
    public void Validate_ShouldFail_WhenContactNameIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithContactName(new string('a', 256)))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.ContactName));
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithEmail("invalid email"))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.Email));
    }

    [Fact]
    public void Validate_ShouldFail_WhenEmailIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithEmail(new string('a', 256) + "@mail.com"))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.Email));
    }

    [Fact]
    public void Validate_ShouldFail_WhenPhoneIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithPhone(new string('a', 256)))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.Phone));
    }

    [Fact]
    public void Validate_ShouldFail_WhenAddressIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithAddress(new string('a', 256)))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.Address));
    }

    [Fact]
    public void Validate_ShouldFail_WhenCityIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithCity(new string('a', 256)))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.City));
    }

    [Fact]
    public void Validate_ShouldFail_WhenZipCodeIsTooLong()
    {
        // Arrange
        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(builder => builder
                .WithZipCode(new string('a', 256)))
            .Build();

        // Act
        TestValidationResult<CreateAgencyWithContactsAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(input.PathFor(
            agency => agency.AgencyContacts,
            contact => contact.ZipCode));
    }
}
