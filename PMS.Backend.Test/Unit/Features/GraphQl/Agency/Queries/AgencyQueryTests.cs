// -----------------------------------------------------------------------
// <copyright file="AgencyQueryTests.cs" company="Vira Vira">
// Copyright (c) Vira Vira. All rights reserved.
// Licensed under the Vira Vira Proprietary License license. See LICENSE.md file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Linq;
using AutoMapper;
using FluentAssertions;
using PMS.Backend.Api.Extensions;
using PMS.Backend.Features.GraphQL.Agency;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;
using PMS.Backend.Features.GraphQL.Agency.Queries;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Fixtures.Agency;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Agency.Queries;

[UnitTest]
public class AgencyQueryTests : AgencyDatabaseFixture
{
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AgencyProfile>()));
    private readonly RecordingLogger<AgencyQuery> _logger = new();
    private readonly AgencyQuery _sut = new();

    [Fact]
    public void AgencyAsync_ShouldReturnAgency()
    {
        // GraphQL is responsible for actually filtering. This query should return all agencies.
        // Act
        IQueryable<AgencyPayload> result = _sut.GetAgency(DbContext, _mapper, _logger);

        // Assert
        result.Should().HaveCount(Entities.Count());
        result.Select(agency => agency.Id).Should().BeEquivalentTo(DbContext.Agencies.Select(agency => agency.Id));

        _logger.ShouldHaveLogged(() => LoggerExtensions.ExecutingQuery);
    }
}
