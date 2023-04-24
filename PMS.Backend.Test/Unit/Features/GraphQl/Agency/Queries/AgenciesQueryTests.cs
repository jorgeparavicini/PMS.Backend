// -----------------------------------------------------------------------
// <copyright file="AgenciesQueryTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Moq;
using PMS.Backend.Core.Database;
using PMS.Backend.Features.GraphQL.Agency;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL.Agency.Queries;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Data;
using PMS.Backend.Test.Extensions;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Queries;

[UnitTest]
public class AgenciesQueryTests
{
    private readonly RecordingLogger<AgenciesQuery> _logger = new();
    private readonly AgenciesQuery _sut = new();
    private readonly Mock<PmsDbContext> _dbContextMock = new();
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AgencyProfile>()));

    [Fact]
    public void AgenciesAsync_ShouldReturnAgencies()
    {
        // Arrange
        const int agencyCount = 5;
        IList<Core.Entities.Agency.Agency> agencies = AgencyData.CreateAgencies(agencyCount).ToList();
        _dbContextMock.Setup(context => context.Agencies).Returns(() => agencies.ToMockDbSet().Object);

        // Act
        IQueryable<AgencyPayload> result = _sut.GetAgencies(_dbContextMock.Object, _mapper, _logger);

        // Assert
        result.Should().HaveCount(agencyCount);
        result.Select(agency => agency.Id).Should().BeEquivalentTo(agencies.Select(agency => agency.Id));

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingQuery);
    }

    [Fact]
    public void AgenciesAsync_ShouldReturnEmptyList()
    {
        // Arrange
        IEnumerable<Core.Entities.Agency.Agency> agencies = new List<Core.Entities.Agency.Agency>();
        _dbContextMock.Setup(context => context.Agencies).Returns(() => agencies.ToMockDbSet().Object);

        // Act
        IQueryable<AgencyPayload> result = _sut.GetAgencies(_dbContextMock.Object, _mapper, _logger);

        // Assert
        result.Should().BeEmpty();
        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingQuery);
    }
}
