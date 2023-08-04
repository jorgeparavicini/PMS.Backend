// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyInputValidatorTests.cs" company="Vira Vira">
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
public class DeleteAgencyInputValidatorTests
{
    private readonly DeleteAgencyInputValidator _sut = new();

    [Fact]
    public void Validate_ShouldSucceed_WhenValidInput()
    {
        // Arrange
        DeleteAgencyInput input = new DeleteAgencyInputBuilder()
            .WithId(Guid.NewGuid())
            .Build();

        // Act
        TestValidationResult<DeleteAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Validate_ShouldFail_WhenIdIsInvalid()
    {
        // Arrange
        DeleteAgencyInput input = new DeleteAgencyInputBuilder()
            .WithId(Guid.Empty)
            .Build();

        // Act
        TestValidationResult<DeleteAgencyInput> result = _sut.TestValidate(input);

        // Assert
        result.ShouldHaveValidationErrorFor(agency => agency.Id);
    }
}
