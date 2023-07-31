// -----------------------------------------------------------------------
// <copyright file="EditAgencyMutationTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
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
using PMS.Backend.Test.Builders.Agency.Models.Input;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Fixtures.Agency;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Mutations;

[UnitTest]
public class EditAgencyMutationTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper;
    private readonly RecordingLogger<EditAgencyMutation> _logger = new();
    private readonly EditAgencyMutation _sut = new();

    public EditAgencyMutationTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AgencyProfile>()).CreateMapper();
    }

    [Fact]
    public async Task EditAgencyAsync_ShouldEditAgency()
    {
        // Arrange
        int currentCount = DbContext.Agencies.Count();
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();

        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithId(agency.Id)
            .Build();

        // Act
        IQueryable<AgencyPayload> result =
            await _sut.EditAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        AgencyPayload? entity = await result.SingleOrDefaultAsync();
        entity.Should().NotBeNull();
        entity!.LegalName.Should().Be(input.LegalName);
        DbContext.Agencies.Count().Should().Be(currentCount);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyEdited);
    }

    [Fact]
    public async Task EditAgencyAsync_ShouldNotModifyAgencyContacts()
    {
        // Arrange
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();

        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithId(agency.Id)
            .Build();

        // Act
        await _sut.EditAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        Core.Entities.Agency.Agency? newAgency = await DbContext.Agencies.FindAsync(agency.Id);
        newAgency.Should().NotBeNull();
        newAgency!.AgencyContacts.Should().BeEquivalentTo(agency.AgencyContacts);
    }

    [Fact]
    public async Task EditAgencyAsync_ShouldThrow_WhenAgencyDoesNotExist()
    {
        // Arrange
        EditAgencyInput input = new EditAgencyInputBuilder()
            .WithId(0)
            .Build();

        // Act
        Func<Task<IQueryable<AgencyPayload>>> act = async () =>
            await _sut.EditAgencyAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"Agency not found with id {input.Id}.");

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldNotHaveLogged(() => Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyEdited);
    }
}
