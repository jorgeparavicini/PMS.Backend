using System;
using System.Collections.Generic;
using PMS.Backend.Features.GraphQL.Agency.Models.Payload;

namespace PMS.Backend.Features.GraphQL.Reservation.Models.Payload;

/// <summary>
///     A collection of reservations made together.
/// </summary>
public record GroupReservationPayload
{
    /// <summary>
    ///     Gets the unique identifier for this entity.
    /// </summary>
    public required Guid Id { get; init; }

    /// <summary>
    ///     Gets an optional reference label to identify the reservation.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    ///     Gets the date when the reservation was made.
    /// </summary>
    public DateTime ReservationDate { get; init; }

    /// <summary>
    ///     Gets a value indicating whether this reservation is a quote.
    /// </summary>
    public bool IsQuote { get; init; }

    /// <summary>
    ///     Gets the id of the associated agency contact.
    /// </summary>
    public Guid AgencyContactId { get; init; }

    /// <summary>
    ///     Gets the contact who made this reservation.
    /// </summary>
    public required AgencyContactPayload AgencyContact { get; init; }

    /// <summary>
    ///     Gets a list of all reservations in this group.
    /// </summary>
    public required IList<ReservationPayload> Reservations { get; init; }
}
