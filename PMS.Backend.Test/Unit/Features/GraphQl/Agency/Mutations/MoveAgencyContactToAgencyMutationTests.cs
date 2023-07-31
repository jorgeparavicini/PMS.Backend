// -----------------------------------------------------------------------
// <copyright file="MoveAgencyContactToAgencyMutationTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL.Agency.Mutations;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Fixtures.Agency;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Mutations;

[UnitTest]
public class MoveAgencyContactToAgencyMutationTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper;
    private readonly RecordingLogger<MoveAgencyContactToAgencyMutation> _logger = new();
    private readonly MoveAgencyContactToAgencyMutation _sut = new();

    public MoveAgencyContactToAgencyMutationTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AgencyProfile>()).CreateMapper();
    }

    [Fact]
    public async Task MoveAgencyContactToAgencyAsync_ShouldMoveAgencyContactToAgency()
    {
        // Arrange
        int agencyCount = DbContext.Agencies.Count();
        int agencyContactCount = DbContext.AgencyContacts.Count();
        List<Core.Entities.Agency.Agency> agencies = DbContext.Agencies.Take(2).ToList();
        int agency1ContactCount = agencies[0].AgencyContacts.Count;
        int agency2ContactCount = agencies[1].AgencyContacts.Count;

        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyId = agencies[0].Id,
            AgencyContactId = agencies[1].AgencyContacts.First().Id,
        };

        // Act
        IQueryable<AgencyPayload> result =
            await _sut.MoveAgencyContactToAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        AgencyPayload? payload = result.SingleOrDefault();
        payload.Should().NotBeNull();
        payload!.Id.Should().Be(agencies[0].Id);

        DbContext.Agencies.Should().HaveCount(agencyCount);
        DbContext.AgencyContacts.Should().HaveCount(agencyContactCount);

        DbContext.Agencies.Single(agency => agency.Id == agencies[0].Id)
            .AgencyContacts.Should()
            .HaveCount(agency1ContactCount + 1);
        DbContext.Agencies.Single(agency => agency.Id == agencies[1].Id)
            .AgencyContacts.Should()
            .HaveCount(agency2ContactCount - 1);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() =>
            Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyContactMovedToAgency);
    }

    [Fact]
    public async Task MoveAgencyContactToAgencyAsync_ShouldThrow_WhenAgencyContactDoesNotExist()
    {
        // Arrange
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();

        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyId = agency.Id,
            AgencyContactId = 0,
        };

        // Act
        Func<Task<IQueryable<AgencyPayload>>> act = async () =>
            await _sut.MoveAgencyContactToAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"AgencyContact not found with id {input.AgencyContactId}.");
    }

    [Fact]
    public async Task MoveAgencyContactToAgencyAsync_ShouldThrow_WhenAgencyDoesNotExist()
    {
        // Arrange
        Core.Entities.Agency.AgencyContact agencyContact = DbContext.AgencyContacts.First();

        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyId = 0,
            AgencyContactId = agencyContact.Id,
        };

        // Act
        Func<Task<IQueryable<AgencyPayload>>> act = async () =>
            await _sut.MoveAgencyContactToAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"Agency not found with id {input.AgencyId}.");
    }

    [Fact]
    public async Task MoveAgencyContactToAgencyAsync_ShouldLog_WhenMovingToSameAgency()
    {
        // Arrange
        Core.Entities.Agency.Agency agency = DbContext.Agencies.Include(agency => agency.AgencyContacts).First();
        Core.Entities.Agency.AgencyContact agencyContact = agency.AgencyContacts.First();

        MoveAgencyContactToAgencyInput input = new()
        {
            AgencyId = agency.Id,
            AgencyContactId = agencyContact.Id,
        };

        // Act
        Func<Task<IQueryable<AgencyPayload>>> act = async () =>
            await _sut.MoveAgencyContactToAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should().NotThrowAsync();
        _logger.ShouldHaveLogged(() =>
            Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyContactIsAlreadyAssignedToAgency);
    }
}
