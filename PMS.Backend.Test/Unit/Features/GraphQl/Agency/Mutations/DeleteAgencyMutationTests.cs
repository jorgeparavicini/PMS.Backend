// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyMutationTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HotChocolate;
using PMS.Backend.Features.GraphQL.Agency.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL.Agency.Mutations;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Common.SqlServer;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Mutations;

[UnitTest]
public class DeleteAgencyMutationTests : TestDatabaseFixture
{
    private readonly RecordingLogger<DeleteAgencyMutation> _logger = new();
    private readonly DeleteAgencyMutation _sut = new();

    [Fact]
    public async Task DeleteAgencyAsync_ShouldDeleteAgency()
    {
        // Arrange
        int currentCount = DbContext.Agencies.Count();
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();
        DeleteAgencyInput input = new()
        {
            Id = agency.Id,
        };

        // Act
        await _sut.DeleteAgencyAsync(DbContext, input, _logger);

        // Assert
        agency.Should().NotBeNull();
        agency.IsDeleted.Should().BeTrue();
        DbContext.Agencies.Should().HaveCount(currentCount - 1);

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => LoggerExtensions.AgencyDeleted);
    }

    [Fact]
    public async Task DeleteAgencyAsync_ShouldDeleteAgencyContacts()
    {
        // Arrange
        int currentCount = DbContext.AgencyContacts.Count();
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();
        DeleteAgencyInput input = new()
        {
            Id = agency.Id,
        };

        // Act
        await _sut.DeleteAgencyAsync(DbContext, input, _logger);

        // Assert
        DbContext.AgencyContacts.Should().HaveCount(currentCount - agency.AgencyContacts.Count);
        agency.AgencyContacts.Should().AllSatisfy(agencyContact => agencyContact.IsDeleted.Should().BeTrue());
    }

    [Fact]
    public async Task DeleteAgencyAsync_ShouldReturnNameOfClient()
    {
        // Arrange
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();
        DeleteAgencyInput input = new()
        {
            Id = agency.Id,
        };

        // Act
        DeleteAgencyPayload result = await _sut.DeleteAgencyAsync(DbContext, input, _logger);

        // Assert
        // TODO: Update to the client id once implemented.
        result.ClientMutationId.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteAgencyAsync_ShouldThrow_WhenAgencyDoesNotExist()
    {
        // Arrange
        DeleteAgencyInput input = new()
        {
            Id = 0,
        };

        // Act
        Func<Task<DeleteAgencyPayload>> act = async () => await _sut.DeleteAgencyAsync(DbContext, input, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"Agency not found with id {input.Id}");
    }
}
