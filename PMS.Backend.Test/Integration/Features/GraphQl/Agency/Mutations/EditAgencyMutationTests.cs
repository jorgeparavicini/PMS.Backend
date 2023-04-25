// -----------------------------------------------------------------------
// <copyright file="EditAgencyMutationTests.cs" company="Vira Vira">
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
public class EditAgencyMutationTests : IClassFixture<AgencyGraphQlFixture>
{
    private readonly AgencyGraphQlFixture _fixture;

    public EditAgencyMutationTests(AgencyGraphQlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task EditAgencyAsync_ShouldReturnEditedAgency()
    {
        // Arrange
        const string mutation = @"

mutation {
  editAgency(
    input: {
        id: 1,
        legalName: ""LegalNamed-55e266c-cb59-421a-88f5-4fc33a409153"",
        commissionMethod: DEDUCTED_BY_PROVIDER,
        defaultCommissionOnExtras: 0.3,
        defaultCommissionRate: 0.2,
        emergencyEmail: ""email@emergency.com"",
        emergencyPhone: ""Phone-d55e266c-cb59-421a-88f5-4fc33a409153"",
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
    public async Task EditAgencyAsync_ShouldReturnError_WhenValidationFails()
    {
        // Arrange
        const string mutation = @"

mutation {
  editAgency(
    input: {
        id: 1,
        legalName: ""LegalNamed-55e266c-cb59-421a-88f5-4fc33a409153"",
        commissionMethod: DEDUCTED_BY_PROVIDER,
        defaultCommissionOnExtras: 15,
        defaultCommissionRate: 0.2,
        emergencyEmail: ""email@emergency.com"",
        emergencyPhone: ""Phone-d55e266c-cb59-421a-88f5-4fc33a409153"",
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
