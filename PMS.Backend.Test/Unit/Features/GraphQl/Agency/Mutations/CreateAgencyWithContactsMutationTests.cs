// -----------------------------------------------------------------------
// <copyright file="CreateAgencyWithContactsMutationTests.cs" company="Vira Vira">
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
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Api.Exceptions;
using PMS.Backend.Api.Extensions;
using PMS.Backend.Api.GraphQL.Agency.Mutations;
using PMS.Backend.Core.Entities.Agency;
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
public class CreateAgencyWithContactsMutationTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper;
    private readonly RecordingLogger<CreateAgencyMutation> _logger = new();
    private readonly CreateAgencyMutation _sut = new();

    public CreateAgencyWithContactsMutationTests()
    {
        // Mapper
        MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<AgencyProfile>());
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateAgencyWithContactsAsync_ShouldCreateAgencyWithContacts()
    {
        // Arrange
        int currentCount = DbContext.Agencies.Count();
        int currentContactCount = DbContext.AgencyContacts.Count();

        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder()
            .AddAgencyContacts(_ => { })
            .Build();

        // Act
        IQueryable<AgencyPayload> result =
            await _sut.CreateAgencyWithContactsAsync(DbContext, input, _mapper, _logger);

        // Assert
        Guid? entityId = await result.Select(agency => agency.Id).SingleOrDefaultAsync();
        entityId.Should().NotBeNull();
        DbContext.Agencies.Count().Should().Be(currentCount + 1);
        DbContext.AgencyContacts.Count().Should().Be(currentContactCount + input.AgencyContacts.Count);

        // Assert that the agency contacts are linked to the agency
        List<AgencyContact> agencyContacts =
            await DbContext.AgencyContacts.Where(contact => contact.AgencyId == entityId).ToListAsync();
        agencyContacts.Should().HaveCount(input.AgencyContacts.Count);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => Api.GraphQL.Agency.Extensions.LoggerExtensions.AgencyCreated);
    }

    [Fact]
    public async Task CreateAgencyWithContactsAsync_ShouldThrow_WhenNoContactsProvided()
    {
        // Arrange
        int currentCount = DbContext.Agencies.Count();
        int currentContactCount = DbContext.AgencyContacts.Count();

        CreateAgencyWithContactsAgencyInput input = new CreateAgencyWithContactsAgencyInputBuilder().Build();

        // Act
        Func<Task<IQueryable<AgencyPayload>>> act = async () =>
            await _sut.CreateAgencyWithContactsAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<EmptyException<AgencyContact>>()
            .WithMessage("At least one AgencyContact must be provided.");

        DbContext.Agencies.Count().Should().Be(currentCount);
        DbContext.AgencyContacts.Count().Should().Be(currentContactCount);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldNotHaveLogged(() => Api.GraphQL.Agency.Extensions.LoggerExtensions.AgencyCreated);
    }
}
