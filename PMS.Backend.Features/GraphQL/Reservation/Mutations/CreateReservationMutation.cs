using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Detached.Mappers.EntityFramework;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.Extensions.Logging;
using PMS.Backend.Core.Database;
using PMS.Backend.Core.Entities.Agency;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.Exceptions;
using PMS.Backend.Features.Extensions;
using PMS.Backend.Features.GraphQL.Reservation.Extensions;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;
using PMS.Backend.Features.GraphQL.Reservation.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Reservation.Mutations;

/// <summary>
///     Represents a GraphQL mutation for creating a new <see cref="GroupReservation"/> entity and returning the
///     corresponding <see cref="GroupReservationPayload"/>.
/// </summary>
[ExtendObjectType<Mutation>]
public class CreateReservationMutation
{
    /// <summary>
    ///     Creates a new <see cref="GroupReservation"/> entity based on the provided input,
    ///     saves the changes to the database, and returns the corresponding <see cref="GroupReservationPayload"/>.
    /// </summary>
    /// <param name="dbContext">
    ///     The <see cref="PmsDbContext"/> instance used to save the new <see cref="GroupReservation"/>.
    /// </param>
    /// <param name="input">
    ///    The input data for creating a new <see cref="GroupReservation"/> entity.
    /// </param>
    /// <param name="mapper">
    ///     The AutoMapper <see cref="IMapper"/> instance used to map the <see cref="GroupReservation"/> entity to the
    ///     <see cref="GroupReservationPayload"/>.
    /// </param>
    /// <param name="logger">
    ///     The <see cref="ILogger"/> instance used for logging.
    /// </param>
    /// <param name="cancellationToken">
    ///     A <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.
    /// </param>
    /// <returns>
    ///     An <see cref="IQueryable"/> of <see cref="GroupReservationPayload"/> representing the newly created
    ///     <see cref="GroupReservation"/> entity.
    /// </returns>
    [HotChocolate.Data.UseSingleOrDefault]
    [UseProjection]
    public async Task<IQueryable<GroupReservationPayload>> CreateReservationAsync(
        PmsDbContext dbContext,
        CreateReservationGroupReservationInput input,
        [Service]
        IMapper mapper,
        [Service]
        ILogger<CreateReservationMutation> logger,
        CancellationToken cancellationToken = default)
    {
        logger.ExecutingMutation(nameof(CreateReservationMutation));

        if (await dbContext.AgencyContacts.FindAsync(
                new object[] { input.AgencyContactId },
                cancellationToken: cancellationToken) is null)
        {
            throw new NotFoundException<AgencyContact>(input.AgencyContactId);
        }

        var reservationEntity = await dbContext.MapAsync<GroupReservation>(input);
        reservationEntity.ReservationDate = DateTime.Today;

        await dbContext.SaveChangesAsync(cancellationToken);

        logger.ReservationCreated(reservationEntity.Id);

        return dbContext.GroupReservations
            .Where(reservation => reservation.Id == reservationEntity.Id)
            .ProjectTo<GroupReservationPayload>(mapper.ConfigurationProvider);
    }
}
