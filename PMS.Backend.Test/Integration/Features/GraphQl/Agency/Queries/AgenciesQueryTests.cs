// -----------------------------------------------------------------------
// <copyright file="AgenciesQueryTests.cs" company="Vira Vira">
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
public class AgenciesQueryTests
{
    private readonly FullDataGraphQlFixture _fixture;

    public AgenciesQueryTests(FullDataGraphQlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ShouldReturnAllAgencies()
    {
        // Arrange
        const string query = @"
            query {
              agencies(first: 2) {
                pageInfo {
                  hasNextPage
                  hasPreviousPage
                }
                nodes {
                  id
                  legalName
                  defaultCommissionRate
                  defaultCommissionOnExtras
                  commissionMethod
                  emergencyPhone
                  emergencyEmail
                  agencyContacts {
                    id
                    contactName
                    email
                    address
                    phone
                    city
                    zipCode
                    isFrequentVendor
                  }
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
