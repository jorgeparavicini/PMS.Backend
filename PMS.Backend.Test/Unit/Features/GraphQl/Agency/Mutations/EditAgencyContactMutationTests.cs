// -----------------------------------------------------------------------
// <copyright file="EditAgencyContactMutationTests.cs" company="Vira Vira">
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
public class EditAgencyContactMutationTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper;
    private readonly RecordingLogger<EditAgencyContactMutation> _logger = new();
    private readonly EditAgencyContactMutation _sut = new();

    public EditAgencyContactMutationTests()
    {
        _mapper = new MapperConfiguration(cfg => cfg.AddProfile<AgencyProfile>()).CreateMapper();
    }

    [Fact]
    public async Task EditAgencyContactAsync_ShouldEditAgencyContact()
    {
        // Arrange
        int currentCount = DbContext.AgencyContacts.Count();
        AgencyContact agencyContact = DbContext.AgencyContacts.First();

        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithId(agencyContact.Id)
            .Build();

        // Act
        IQueryable<AgencyContactPayload> result =
            await _sut.EditAgencyContactAsync(DbContext, input, _mapper, _logger);

        // Assert
        AgencyContactPayload? entity = await result.SingleOrDefaultAsync();
        entity.Should().NotBeNull();
        entity!.ContactName.Should().Be(input.ContactName);
        DbContext.AgencyContacts.Count().Should().Be(currentCount);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldHaveLogged(() =>
            Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyContactEdited);
    }

    [Fact]
    public async Task EditAgencyContactAsync_ShouldThrowError_WhenContactDoesNotExist()
    {
        // Arrange
        int currentCount = DbContext.AgencyContacts.Count();

        EditAgencyContactInput input = new EditAgencyContactInputBuilder()
            .WithId(0)
            .Build();

        // Act
        Func<Task<IQueryable<AgencyContactPayload>>> act = async () =>
            await _sut.EditAgencyContactAsync(DbContext, input, _mapper, _logger);

        // Assert
        await act.Should()
            .ThrowAsync<GraphQLException>()
            .WithMessage($"AgencyContact not found with id {input.Id}.");
        DbContext.AgencyContacts.Count().Should().Be(currentCount);

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingMutation);
        _logger.ShouldNotHaveLogged(() =>
            Backend.Features.GraphQL.Agency.Extensions.LoggerExtensions.AgencyContactEdited);
    }
}
