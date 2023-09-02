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
///     A GraphQL query for searching a single <see cref="GroupReservationPayload"/> based on specified filtering criteria.
/// </summary>
[ExtendObjectType<Query>]
public class ReservationQuery
{
    /// <summary>
    ///     Retrieves a <see cref="GroupReservationPayload"/> based on the specified filtering criteria.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to query the database.
    /// </param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the <see cref="GroupReservation"/> entity
    ///     to the <see cref="GroupReservationPayload"/>.
    /// </param>
    /// <param name="logger">
    ///     The <see cref="ILogger"/> instance used for logging.
    /// </param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="GroupReservationPayload"/> representing the first
    ///     <see cref="GroupReservation"/> that matches the filtering criteria, or null if no
    ///     <see cref="GroupReservation"/> matches the criteria.
    /// </returns>
    [HotChocolate.Data.UseFirstOrDefault]
    [UseProjection]
    [HotChocolate.Data.UseFiltering]
    public IQueryable<GroupReservationPayload> GetReservation(
        PmsDbContext dbContext,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<ReservationQuery> logger)
    {
        logger.ExecutingQuery(nameof(ReservationQuery));
        return dbContext.GroupReservations.ProjectTo<GroupReservationPayload>(mapper.ConfigurationProvider);
    }
}
