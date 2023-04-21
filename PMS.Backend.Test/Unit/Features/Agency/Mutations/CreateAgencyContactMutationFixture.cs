// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactMutationFixture.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoMapper;
using Detached.Mappers.EntityFramework;
using FluentAssertions;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Features.Agency;
using PMS.Backend.Features.Features.Agency.Extensions;
using PMS.Backend.Features.Features.Agency.Models.Input;
using PMS.Backend.Features.Features.Agency.Models.Payload;
using PMS.Backend.Features.Features.Agency.Mutations;
using PMS.Backend.Test.Common.Logging;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.Agency.Mutations;

[UnitTest]
public class CreateAgencyContactMutationFixture
{
    private readonly PmsDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly RecordingLogger<CreateAgencyContactMutation> _logger = new();
    private readonly CreateAgencyContactMutation _sut = new();

    public CreateAgencyContactMutationFixture()
    {
        // DbContext
        DbContextOptions<PmsDbContext> options = new DbContextOptionsBuilder<PmsDbContext>()
            .UseInMemoryDatabase(databaseName: "CreateAgencyContactMutationFixture")
            .UseDetached()
            .Options;
        _dbContext = new PmsDbContext(options);

        // Mapper
        MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<AgencyProfile>());
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateAgencyContactAsync_ShouldCreateAgencyContact()
    {
        // Arrange
        Fixture fixture = new();
        Core.Entities.Agency.Agency agency = fixture.Build<Core.Entities.Agency.Agency>()
            .With(agency => agency.AgencyContacts, () => new List<AgencyContact>())
            .Create();

        await _dbContext.Agencies.AddAsync(agency);
        await _dbContext.SaveChangesAsync();

        CreateAgencyContactInput input = fixture.Build<CreateAgencyContactInput>()
            .With(input => input.AgencyId, agency.Id)
            .Create();

        // Act
        IQueryable<AgencyContactPayload> result =
            await _sut.CreateAgencyContactAsync(_dbContext, input, _mapper, _logger);

        // Assert
        int? entityId = await result.Select(contact => contact.Id).SingleOrDefaultAsync();
        Assert.NotNull(entityId);

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => LoggerExtensions.AgencyContactCreated);
    }

    [Fact]
    public async Task CreateAgencyContactAsync_ShouldThrowNotFoundError_WhenAgencyDoesNotExist()
    {
        // Arrange
        Fixture fixture = new();
        CreateAgencyContactInput input = fixture.Build<CreateAgencyContactInput>()
            .With(input => input.AgencyId, 0)
            .Create();

        // Act
        Func<Task<IQueryable<AgencyContactPayload>>> act = async () =>
            await _sut.CreateAgencyContactAsync(_dbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"Agency not found with id {input.AgencyId}.");

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingMutation);
        _logger.ShouldNotHaveLogged(() => LoggerExtensions.AgencyContactCreated);
    }
}
