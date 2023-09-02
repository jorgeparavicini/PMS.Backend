using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Reservation.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Reservation.Queries;

/// <summary>
///     A GraphQL query for paging <see cref="GroupReservationPayload"/> based on specified filtering criteria.
/// </summary>
[ExtendObjectType<Query>]
public class ReservationsQuery
{
    /// <summary>
    ///     Retrieves a paginated list of <see cref="GroupReservationPayload"/> based on the specified sorting, filtering,
    ///     and projection criteria.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to query the database.
    /// </param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the <see cref="GroupReservation"/> entity.
    /// </param>
    /// <param name="logger">
    ///     The <see cref="ILogger"/> instance used for logging.
    /// </param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="GroupReservationPayload"/>s representing the reservations that match the
    ///     specified sorting, filtering, and projection criteria.
    /// </returns>
    [UsePaging]
    [UseProjection]
    [HotChocolate.Data.UseSorting]
    [HotChocolate.Data.UseFiltering]
    public IQueryable<GroupReservationPayload> GetReservations(
        PmsDbContext dbContext,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<ReservationsQuery> logger)
    {
        logger.ExecutingQuery(nameof(ReservationsQuery));
        return dbContext.GroupReservations.ProjectTo<GroupReservationPayload>(mapper.ConfigurationProvider);
    }
}
