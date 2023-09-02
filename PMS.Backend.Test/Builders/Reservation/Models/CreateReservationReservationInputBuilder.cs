using System;
using System.Collections.Generic;
using System.Linq;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;

namespace PMS.Backend.Test.Builders.Reservation.Models;

public class CreateReservationReservationInputBuilder
{
    private string? _name;

    private IList<Action<CreateReservationReservationDetailsInputBuilder>> _reservationDetails =
        new List<Action<CreateReservationReservationDetailsInputBuilder>>
        {
            _ => { },
        };

    public CreateReservationReservationInputBuilder WithName(string? name)
    {
        _name = name;
        return this;
    }

    public CreateReservationReservationInputBuilder WithReservationDetails(
        params Action<CreateReservationReservationDetailsInputBuilder>[] reservationDetailBuilders)
    {
        _reservationDetails = reservationDetailBuilders;
        return this;
    }

    public CreateReservationReservationInput Build() => new()
    {
        Name = _name,
        ReservationDetails = _reservationDetails.Select(action =>
            {
                var builder = new CreateReservationReservationDetailsInputBuilder();
                action(builder);
                return builder.Build();
            })
            .ToList(),
    };
}
