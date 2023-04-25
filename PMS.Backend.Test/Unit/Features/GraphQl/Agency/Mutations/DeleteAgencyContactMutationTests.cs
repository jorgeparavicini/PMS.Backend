// -----------------------------------------------------------------------
// <copyright file="DeleteAgencyContactMutationTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HotChocolate;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.GraphQL.Agency.Extensions;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL.Agency.Mutations;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Data;
using PMS.Backend.Test.Fixtures;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Mutations;

[UnitTest]
public class DeleteAgencyContactMutationTests : AgencyDatabaseFixture
{
    private readonly RecordingLogger<DeleteAgencyContactMutation> _logger = new();
    private readonly DeleteAgencyContactMutation _sut = new();

    [Fact]
    public async Task DeleteAgencyContactAsync_ShouldDeleteAgencyContact()
    {
        // Arrange
        int currentCount = DbContext.AgencyContacts.Count();
        AgencyContact contact = DbContext.AgencyContacts.First();
        DeleteAgencyContactInput input = new()
        {
            Id = contact.Id,
        };

        // Act
        await _sut.DeleteAgencyContactAsync(DbContext, input, _logger);

        // Assert
        contact.Should().NotBeNull();
        contact.IsDeleted.Should().BeTrue();
        DbContext.AgencyContacts.Should().HaveCount(currentCount - 1);

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => LoggerExtensions.AgencyContactDeleted);
    }

    [Fact]
    public async Task DeleteAgencyContactAsync_ShouldReturnNameOfClient()
    {
        // Arrange
        AgencyContact contact = DbContext.AgencyContacts.First();
        DeleteAgencyContactInput input = new()
        {
            Id = contact.Id,
        };

        // Act
        DeleteAgencyContactPayload result = await _sut.DeleteAgencyContactAsync(DbContext, input, _logger);

        // Assert
        // TODO: Update to the client id once implemented.
        result.ClientMutationId.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteAgencyContactAsync_ShouldThrow_WhenContactDoesNotExist()
    {
        // Arrange
        DeleteAgencyContactInput input = new()
        {
            Id = 0,
        };

        // Act
        Func<Task<DeleteAgencyContactPayload>> result = async () =>
            await _sut.DeleteAgencyContactAsync(DbContext, input, _logger);

        // Assert
        await result.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"Agency contact not found with id {input.Id}");
    }

    [Fact]
    public async Task DeleteAgencyContactAsync_ShouldThrow_WhenIsLastContact()
    {
        // Arrange
        Core.Entities.Agency.Agency agency = AgencyData.CreateAgency();
        agency.AgencyContacts = new List<AgencyContact>()
        {
            AgencyData.CreateAgencyContact(),
        };
        DbContext.Agencies.Add(agency);
        await DbContext.SaveChangesAsync();

        DeleteAgencyContactInput input = new()
        {
            Id = agency.AgencyContacts.Single().Id,
        };

        // Act
        Func<Task<DeleteAgencyContactPayload>> result = async () =>
            await _sut.DeleteAgencyContactAsync(DbContext, input, _logger);

        // Assert
        await result.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage(
                "Cannot delete the last contact for an agency. Please add a new contact before deleting this one.");
    }
}
