using System.Collections.Generic;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.GraphQL.Reservation.Mutations;

namespace PMS.Backend.Features.GraphQL.Reservation.Models.Input;

/// <summary>
///     The <see cref="GroupReservation"/> input for the <see cref="CreateReservationMutation"/>.
/// </summary>
public record CreateReservationGroupReservationInput
{
    /// <summary>
    ///     Gets the optional reference label to identify this reservation.
    /// </summary>
    public string? Reference { get; init; }

    /// <summary>
    ///     Gets a value indicating whether this reservation is a quote.
    /// </summary>
    public bool IsQuote { get; init; }

    /// <summary>
    ///     Gets the id of the agency contact who made this reservation.
    /// </summary>
    public int AgencyContactId { get; init; }

    /// <summary>
    ///     Gets the list of all reservations in this group.
    /// </summary>
    public required IList<CreateReservationReservationInput> Reservations { get; init; }
}
