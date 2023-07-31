using System.Collections.Generic;
using PMS.Backend.Features.GraphQL.Reservation.Mutations;

namespace PMS.Backend.Features.GraphQL.Reservation.Models.Input;

/// <summary>
///     The <see cref="Reservation"/> input for the <see cref="CreateReservationMutation"/>.
/// </summary>
public record CreateReservationReservationInput
{
    /// <summary>
    ///     Gets the optional name of this reservation.
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Gets a list of all reservation details in this reservation.
    /// </summary>
    public required IList<CreateReservationReservationDetailsInput> ReservationDetails { get; init; }
}
