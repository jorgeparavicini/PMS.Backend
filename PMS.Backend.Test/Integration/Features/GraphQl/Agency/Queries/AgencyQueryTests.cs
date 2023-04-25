// -----------------------------------------------------------------------
// <copyright file="AgencyQueryTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Threading.Tasks;
using HotChocolate.Execution;
using PMS.Backend.Test.Collections;
using PMS.Backend.Test.Fixtures;
using VerifyXunit;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Integration.Features.GraphQl.Agency.Queries;

[IntegrationTest]
[UsesVerify]
[Collection(CollectionIndex.ReadonlyCollection)]
public class AgencyQueryTests
{
    private readonly FullDataGraphQlFixture _fixture;

    public AgencyQueryTests(FullDataGraphQlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetAgency_ShouldReturnSingleAgency()
    {
        // Arrange
        const string query = @"
            query {
                agency(where: {id: {eq: 1}}) {
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
            }
        ";

        QueryRequestBuilder queryRequestBuilder = new();
        queryRequestBuilder.SetQuery(query);

        // Act
        await using IExecutionResult result = await _fixture.Executor.ExecuteAsync(queryRequestBuilder.Create());

        // Assert
        result.ExpectQueryResult();
        await Verifier.Verify(result);
    }
}
