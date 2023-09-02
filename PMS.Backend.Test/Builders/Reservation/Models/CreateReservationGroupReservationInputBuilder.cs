using System;
using System.Collections.Generic;
using System.Linq;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;

namespace PMS.Backend.Test.Builders.Reservation.Models;

public class CreateReservationGroupReservationInputBuilder
{
    private string? _reference;
    private bool _isQuote;
    private Guid _agencyContactId;

    private IList<Action<CreateReservationReservationInputBuilder>> _reservations =
        new List<Action<CreateReservationReservationInputBuilder>>
        {
            _ => { },
        };

    public CreateReservationGroupReservationInputBuilder WithReference(string? reference)
    {
        _reference = reference;
        return this;
    }

    public CreateReservationGroupReservationInputBuilder WithIsQuote(bool isQuote)
    {
        _isQuote = isQuote;
        return this;
    }

    public CreateReservationGroupReservationInputBuilder WithAgencyContactId(Guid agencyContactId)
    {
        _agencyContactId = agencyContactId;
        return this;
    }

    public CreateReservationGroupReservationInputBuilder WithReservations(
        params Action<CreateReservationReservationInputBuilder>[] reservationBuilders)
    {
        _reservations = reservationBuilders;
        return this;
    }

    public CreateReservationGroupReservationInput Build() => new()
    {
        Reference = _reference,
        IsQuote = _isQuote,
        AgencyContactId = _agencyContactId,
        Reservations = _reservations.Select(action =>
            {
                var builder = new CreateReservationReservationInputBuilder();
                action(builder);
                return builder.Build();
            })
            .ToList(),
    };
}
