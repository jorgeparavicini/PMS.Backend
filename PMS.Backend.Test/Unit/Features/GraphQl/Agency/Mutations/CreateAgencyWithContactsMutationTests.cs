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
using AutoFixture;
using AutoMapper;
using FluentAssertions;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Agency;
using PMS.Backend.Features.GraphQL.Agency.Models.Input;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL.Agency.Mutations;
using PMS.Backend.Test.Common.Customization;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Fixtures;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Mutations;

[UnitTest]
public class CreateAgencyWithContactsMutationTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper;
    private readonly RecordingLogger<CreateAgencyWithContactsMutation> _logger = new();
    private readonly CreateAgencyWithContactsMutation _sut = new();
    private readonly Fixture _fixture = new();

    public CreateAgencyWithContactsMutationTests()
    {
        // Mapper
        MapperConfiguration mapperConfig = new(cfg => cfg.AddProfile<AgencyProfile>());
        _mapper = mapperConfig.CreateMapper();

        _fixture.Customize(new CommissionCustomization<CreateAgencyWithContactsInput>(
            agency => agency.DefaultCommissionRate,
            agency => agency.DefaultCommissionOnExtras));
    }

    [Fact]
    public async Task CreateAgencyWithContactsAsync_ShouldCreateAgencyWithContacts()
    {
        // Arrange
        int currentCount = DbContext.Agencies.Count();
        int currentContactCount = DbContext.AgencyContacts.Count();

        var input = _fixture.Create<CreateAgencyWithContactsInput>();

        // Act
        IQueryable<AgencyPayload> result =
            await _sut.CreateAgencyWithContactsAsync(DbContext, input, _mapper, _logger);

        // Assert
        int? entityId = await result.Select(agency => agency.Id).SingleOrDefaultAsync();
        entityId.Should().NotBeNull();
        DbContext.Agencies.Count().Should().Be(currentCount + 1);
        DbContext.AgencyContacts.Count().Should().Be(currentContactCount + input.AgencyContacts.Count);

        // Assert that the agency contacts are linked to the agency
        List<AgencyContact> agencyContacts =
            await DbContext.AgencyContacts.Where(contact => contact.AgencyId == entityId).ToListAsync();
        agencyContacts.Should().HaveCount(input.AgencyContacts.Count);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() => Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyCreated);
    }

    [Fact]
    public async Task CreateAgencyWithContactsAsync_ShouldThrow_WhenNoContactsProvided()
    {
        // Arrange
        int currentCount = DbContext.Agencies.Count();
        int currentContactCount = DbContext.AgencyContacts.Count();

        CreateAgencyWithContactsInput input = _fixture.Build<CreateAgencyWithContactsInput>()
            .With(input => input.AgencyContacts, new List<AgencyContactForCreateAgencyWithContactsInput>())
            .Create();

        // Act
        Func<Task<IQueryable<AgencyPayload>>> act = async () =>
            await _sut.CreateAgencyWithContactsAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage("At least one agency contact must be provided.");

        DbContext.Agencies.Count().Should().Be(currentCount);
        DbContext.AgencyContacts.Count().Should().Be(currentContactCount);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldNotHaveLogged(() => Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyCreated);
    }
}
