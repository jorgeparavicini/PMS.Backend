// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyMutationTest.cs" company="Vira Vira">
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
public class DeleteAgencyMutationTest : IClassFixture<AgencyGraphQlFixture>
{
    private readonly AgencyGraphQlFixture _fixture;

    public DeleteAgencyMutationTest(AgencyGraphQlFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeleteAgencyAsync_ShouldDeleteAgency()
    {
        // Arrange
        const string mutation = @"
mutation {
  deleteAgency(input: {id: 1}) {
    clientMutationId
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
