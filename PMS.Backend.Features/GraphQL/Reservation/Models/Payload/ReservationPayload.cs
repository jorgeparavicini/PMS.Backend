using System.Collections.Generic;

namespace PMS.Backend.Features.GraphQL.Reservation.Models.Payload;

/// <summary>
///     A single reservation in a <see cref="GroupReservationPayload"/> that holds a list of all details for this reservation.
/// </summary>
public record ReservationPayload
{
    /// <summary>
    ///     Gets the unique identifier for this entity.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    ///     Gets an optional name label to identify the reservation.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Gets the id of the group reservation this reservation is part of.
    /// </summary>
    public int GroupReservationId { get; init; }

    /// <summary>
    ///     Gets the parent group reservation.
    /// </summary>
    public required GroupReservationPayload GroupReservation { get; init; }

    /// <summary>
    ///     Gets a list of all details in this reservation.
    /// </summary>
    public required IList<ReservationDetailPayload> ReservationDetails { get; init; }
}
