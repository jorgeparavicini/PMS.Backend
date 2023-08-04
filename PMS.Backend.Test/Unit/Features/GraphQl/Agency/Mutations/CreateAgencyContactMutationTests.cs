// -----------------------------------------------------------------------
// <copyright file="CreateAgencyContactMutationTests.cs" company="Vira Vira">
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
using PMS.Backend.Features.GraphQL.Agency;
using PMS.Backend.Features.GraphQL.Agency.Extensions;
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
public class CreateAgencyContactMutationTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper;
    private readonly RecordingLogger<CreateAgencyContactMutation> _logger = new();
    private readonly CreateAgencyContactMutation _sut = new();

    public CreateAgencyContactMutationTests()
    {
        // Mapper
        MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<AgencyProfile>());
        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task CreateAgencyContactAsync_ShouldCreateAgencyContact()
    {
        // Arrange
        int currentCount = DbContext.AgencyContacts.Count();
        Core.Entities.Agency.Agency agency = DbContext.Agencies.First();

        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(agency.Id)
            .Build();

        // Act
        IQueryable<AgencyContactPayload> result =
            await _sut.CreateAgencyContactAsync(DbContext, input, _mapper, _logger);

        // Assert
        Guid? entityId = await result.Select(contact => contact.Id).SingleOrDefaultAsync();
        entityId.Should().NotBeNull();
        DbContext.AgencyContacts.Should().HaveCount(currentCount + 1);

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => LoggerExtensions.AgencyContactCreated);
    }

    [Fact]
    public async Task CreateAgencyContactAsync_ShouldThrowNotFoundError_WhenAgencyDoesNotExist()
    {
        // Arrange
        CreateAgencyContactInput input = new CreateAgencyContactInputBuilder()
            .WithAgencyId(Guid.Empty)
            .Build();

        // Act
        Func<Task<IQueryable<AgencyContactPayload>>> act = async () =>
            await _sut.CreateAgencyContactAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"Agency not found with id {input.AgencyId}.");

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingMutation);
        _logger.ShouldNotHaveLogged(() => LoggerExtensions.AgencyContactCreated);
    }
}
