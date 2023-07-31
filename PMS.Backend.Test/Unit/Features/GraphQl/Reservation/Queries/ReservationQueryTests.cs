using System.Linq;
using AutoMapper;
using FluentAssertions;
using PMS.Backend.Features.GraphQL.Reservation;
using PMS.Backend.Features.GraphQL.Reservation.Models.Payload;
using PMS.Backend.Features.GraphQL.Reservation.Queries;
using PMS.Backend.Test.Common.Logging;
using PMS.Backend.Test.Fixtures.Reservation;
using Xunit;
using Xunit.Categories;

namespace PMS.Backend.Test.Unit.Features.GraphQl.Reservation.Queries;

[UnitTest]
public class ReservationQueryTests : ReservationDatabaseFixture
{
    private readonly IMapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<ReservationProfile>()));
    private readonly RecordingLogger<ReservationQuery> _logger = new();
    private readonly ReservationQuery _sut = new();

    [Fact]
    public void ReservationAsync_ShouldReturnReservation()
    {
        // Act
        IQueryable<GroupReservationPayload> result = _sut.GetReservation(DbContext, _mapper, _logger);

        // Assert
        result.Should().HaveCount(Entities.Count());
        result.Select(reservation => reservation.Id)
            .Should()
            .BeEquivalentTo(DbContext.Reservations.Select(reservation => reservation.Id));

        _logger.ShouldHaveLogged(() => Backend.Features.Extensions.LoggerExtensions.ExecutingQuery);
    }
}
