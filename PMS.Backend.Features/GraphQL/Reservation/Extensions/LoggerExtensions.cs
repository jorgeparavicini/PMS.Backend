using Microsoft.Extensions.Logging;

namespace PMS.Backend.Features.GraphQL.Reservation.Extensions;

/// <summary>
///     Extensions for <see cref="ILogger"/>.
/// </summary>
public static partial class LoggerExtensions
{
    /// <summary>
    ///     Logs the creation of a reservation.
    /// </summary>
    /// <param name="logger">
    ///     The <see cref="ILogger"/> instance used for logging.
    /// </param>
    /// <param name="reservationId">
    ///     The ID of the <see cref="Reservation"/> entity that was created.
    /// </param>
    [LoggerMessage(
        eventId: 10201,
        LogLevel.Information,
        message: "Created reservation {ReservationId}",
        EventName = nameof(ReservationCreated))]
    public static partial void ReservationCreated(this ILogger logger, int reservationId);
}
