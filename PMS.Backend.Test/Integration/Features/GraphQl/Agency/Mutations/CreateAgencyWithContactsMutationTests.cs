// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsMutationTests.cs" company="Vira Vira">
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
public class CreateAgencyWithContactsMutationTests : IClassFixture<AgencyGraphQlDatabaseIntegrationFixture>
{
    private readonly AgencyGraphQlDatabaseIntegrationFixture _fixture;

    public CreateAgencyWithContactsMutationTests(
        AgencyGraphQlDatabaseIntegrationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateAgencyWithContactsAsync_ShouldReturnCreatedAgencyWithContacts()
    {
        // Arrange
        const string mutation = @"

mutation {
  createAgencyWithContacts(
    input: {
      legalName: ""LegalNamed-55e266c-cb59-421a-88f5-4fc33a409153"",
        commissionMethod: DEDUCTED_BY_PROVIDER,
        defaultCommissionOnExtras: 0.1,
        defaultCommissionRate: 0.2,
        emergencyEmail: ""email@emergency.com"",
        emergencyPhone: ""Phone-d55e266c-cb59-421a-88f5-4fc33a409153"",
        agencyContacts: [
        {
            contactName: ""Contact Name - d55e266c-cb59-421a-88f5-4fc33a409153""
            address: ""Address - d55e266c-cb59-421a-88f5-4fc33a409153""
            city: ""City - d55e266c-cb59-421a-88f5-4fc33a409153""
            email: ""d55e266c-cb59-421a-88f5-4fc33a409153@gmail.com"",
            phone: ""Phone-d55e266c-cb59-421a-88f5-4fc33a409153"",
            zipCode: ""Zip-d55e266c-cb59-421a-88f5-4fc33a409153"",
            isFrequentVendor: true
        },
        {
            contactName: ""Contact Name - 7196e2db-38c5-4b17-89a1-d6d54740836b""
            address: ""Address - 7196e2db-38c5-4b17-89a1-d6d54740836b""
            city: ""City - 7196e2db-38c5-4b17-89a1-d6d54740836b""
            email: ""7196e2db-38c5-4b17-89a1-d6d54740836b@gmail.com"",
            phone: ""Phone - 7196e2db-38c5-4b17-89a1-d6d54740836b"",
            zipCode: ""Zip - 7196e2db-38c5-4b17-89a1-d6d54740836b"",
            isFrequentVendor: false
        }
        ]
    }
    ) {
        id,
        legalName,
        defaultCommissionRate,
        defaultCommissionOnExtras,
        commissionMethod,
        emergencyPhone,
        emergencyEmail,
        agencyContacts {
            id,
            contactName,
            email,
            address,
            phone,
            city,
            zipCode,
            isFrequentVendor
        }
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
    public async Task CreateAgencyWithContactsAsync_ShouldReturnError_WhenValidationFails()
    {
        // Arrange
        const string mutation = @"

mutation {
  createAgencyWithContacts(
    input: {
      legalName: ""LegalNamed-55e266c-cb59-421a-88f5-4fc33a409153"",
        commissionMethod: DEDUCTED_BY_PROVIDER,
        defaultCommissionOnExtras: 15,
        defaultCommissionRate: 0.2,
        emergencyEmail: ""email@emergency.com"",
        emergencyPhone: ""Phone-d55e266c-cb59-421a-88f5-4fc33a409153"",
        agencyContacts: [
        {
            contactName: ""Contact Name - d55e266c-cb59-421a-88f5-4fc33a409153""
            address: ""Address - d55e266c-cb59-421a-88f5-4fc33a409153""
            city: ""City - d55e266c-cb59-421a-88f5-4fc33a409153""
            email: ""d55e266c-cb59-421a-88f5-4fc33a409153@gmail.com"",
            phone: ""Phone-d55e266c-cb59-421a-88f5-4fc33a409153"",
            zipCode: ""Zip-d55e266c-cb59-421a-88f5-4fc33a409153"",
            isFrequentVendor: true
        },
        {
            contactName: ""Contact Name - 7196e2db-38c5-4b17-89a1-d6d54740836b""
            address: ""Address - 7196e2db-38c5-4b17-89a1-d6d54740836b""
            city: ""City - 7196e2db-38c5-4b17-89a1-d6d54740836b""
            email: ""7196e2db-38c5-4b17-89a1-d6d54740836b@gmail.com"",
            phone: ""Phone - 7196e2db-38c5-4b17-89a1-d6d54740836b"",
            zipCode: ""Zip - 7196e2db-38c5-4b17-89a1-d6d54740836b"",
            isFrequentVendor: false
        }
        ]
    }
    ) {
        id,
        legalName,
        defaultCommissionRate,
        defaultCommissionOnExtras,
        commissionMethod,
        emergencyPhone,
        emergencyEmail,
        agencyContacts {
            id,
            contactName,
            email,
            address,
            phone,
            city,
            zipCode,
            isFrequentVendor
        }
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
