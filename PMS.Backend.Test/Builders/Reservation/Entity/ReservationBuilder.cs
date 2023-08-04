using System;
using System.Collections.Generic;
using System.Linq;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Test.Builders.Reservation.Entity;

public class ReservationBuilder
{
    private Guid _id;
    private string? _name;

    private IList<Action<ReservationDetailBuilder>> _reservationDetails =
        new List<Action<ReservationDetailBuilder>>
        {
            _ => { },
        };

    public ReservationBuilder WithId(Guid id)
    {
        _id = id;
        return this;
    }

    public ReservationBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public ReservationBuilder WithReservationDetails(
        params Action<ReservationDetailBuilder>[] reservationDetailBuilders)
    {
        _reservationDetails = reservationDetailBuilders;
        return this;
    }

    public Core.Entities.Reservation.Reservation Build(Guid groupReservationId, GroupReservation groupReservation)
    {
        Core.Entities.Reservation.Reservation reservation = new()
        {
            Id = _id,
            Name = _name,
            GroupReservationId = groupReservationId,
            GroupReservation = groupReservation,
            ReservationDetails = null!,
        };

        reservation.ReservationDetails = _reservationDetails.Select(action =>
            {
                var builder = new ReservationDetailBuilder();
                action(builder);
                return builder.Build(reservation.Id, reservation);
            })
            .ToList();

        return reservation;
    }
}
