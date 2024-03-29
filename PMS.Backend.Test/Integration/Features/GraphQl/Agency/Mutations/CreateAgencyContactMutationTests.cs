﻿// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactMutationTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using PMS.Backend.Test.Fixtures.Agency;
using VerifyXunit;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Integration.Features.GraphQl.Agency.Mutations;

[IntegrationTest]
[UsesVerify]
public class CreateAgencyContactMutationTests : IClassFixture<AgencyGraphQlFixture>
{
    private readonly AgencyGraphQlFixture _fixture;

    public CreateAgencyContactMutationTests(AgencyGraphQlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateAgencyContactAsync_ShouldReturnCreatedAgencyContact()
    {
        // Arrange
        const string mutation = @"
mutation {
  createAgencyContact(
    input: {
      agencyId: 2
      contactName: ""Legal Entity""
      address: ""New Contact Address""
      city: ""Contact123"",
      email: ""email@email.com"",
      phone: ""Phone123"",
      zipCode: ""Zip123"",
      isFrequentVendor: true
    }
    ) {
    id
    contactName
    address
    city
    email
    phone
    zipCode
    isFrequentVendor
    }
}";
        QueryRequestBuilder queryRequestBuilder = new();
        queryRequestBuilder.SetQuery(mutation);

        // Act
        await using IExecutionResult result = await _fixture.Executor.ExecuteAsync(queryRequestBuilder.Create());

        // Assert
        result.ExpectQueryResult();
        await Verifier.Verify(result.ToJson());
    }

    [Fact]
    public async Task CreateAgencyContactAsync_ShouldReturnError_WhenValidationFails()
    {
        // Arrange
        const string mutation = @"
mutation {
  createAgencyContact(
    input: {
      agencyId: 3
      contactName: ""Legal Entity""
      address: ""New Contact Address""
      city: ""Contact123"",
      email: ""this is not an email"",
      phone: ""Phone123"",
      zipCode: ""Zip123"",
      isFrequentVendor: true
    }
    ) {
    id
    contactName
    address
    city
    email
    phone
    zipCode
    isFrequentVendor
    }
}";
        QueryRequestBuilder queryRequestBuilder = new();
        queryRequestBuilder.SetQuery(mutation);

        // Act
        await using IExecutionResult result = await _fixture.Executor.ExecuteAsync(queryRequestBuilder.Create());

        // Assert
        result.ExpectQueryResult();
        await Verifier.Verify(result.ToJson());
    }
}
