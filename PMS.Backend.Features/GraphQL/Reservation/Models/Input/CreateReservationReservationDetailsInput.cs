using System;
using PMS.Backend.Core.Entities.Reservation;
using PMS.Backend.Features.GraphQL.Reservation.Mutations;

namespace PMS.Backend.Features.GraphQL.Reservation.Models.Input;

/// <summary>
///     The <see cref="ReservationDetail"/>s input for the <see cref="CreateReservationMutation"/>.
/// </summary>
public record CreateReservationReservationDetailsInput
{
    /// <summary>
    ///     Gets the date of the check-in.
    /// </summary>
    public DateOnly CheckIn { get; init; }

    /// <summary>
    ///     Gets the date of the check-out.
    /// </summary>
    public DateOnly CheckOut { get; init; }
}
