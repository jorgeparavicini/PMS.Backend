using System;

namespace PMS.Backend.Features.GraphQL.Reservation.Models.Payload;

/// <summary>
///     A reservation detail in a <see cref="ReservationPayload"/> that holds the details for this reservation.
/// </summary>
public record ReservationDetailPayload
{
    /// <summary>
    ///     Gets the unique identifier for this entity.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    ///     Gets the date when the reservation was made.
    /// </summary>
    public DateTime ReservationDate { get; init; }

    /// <summary>
    ///     Gets the date for the check-in.
    /// </summary>
    public DateOnly CheckIn { get; init; }

    /// <summary>
    ///     Gets the date for the check-out.
    /// </summary>
    public DateOnly CheckOut { get; init; }

    /// <summary>
    ///     Gets the date when the folio was closed. Null if the folio is still open.
    /// </summary>
    public DateTime? FolioClosedOn { get; init; }

    /// <summary>
    ///     Gets the id of the reservation this detail is part of.
    /// </summary>
    public Guid ReservationId { get; init; }

    /// <summary>
    ///     Gets the parent reservation.
    /// </summary>
    public required ReservationPayload Reservation { get; init; }
}
