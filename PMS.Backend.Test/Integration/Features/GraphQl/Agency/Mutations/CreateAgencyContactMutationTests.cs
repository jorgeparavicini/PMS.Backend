// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactMutationTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.Execution;
using PMS.Backend.Test.Fixtures;
using VerifyXunit;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Integration.Features.GraphQl.Agency.Mutations;

[IntegrationTest]
[UsesVerify]
public class CreateAgencyContactMutationTests : AgencyGraphQlDatabaseIntegrationFixture
{
    [Fact]
    public async Task CreateAgencyContactMutation_ShouldReturnCreatedAgencyContact()
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

        // Act
        QueryRequestBuilder queryRequestBuilder = new();
        queryRequestBuilder.SetQuery(mutation);
        await using IExecutionResult result = await Executor.ExecuteAsync(queryRequestBuilder.Create());

        // Assert
        result.ExpectQueryResult();
        await Verifier.Verify(result.ToJson());
    }
}
