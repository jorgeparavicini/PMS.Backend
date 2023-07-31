using System;
using System.Collections.Generic;
using System.Linq;

namespace PMS.Backend.Test.Builders.Reservation.Entity;

public class GroupReservationBuilder
{
    private int _id;
    private string? _reference;
    private DateTime _reservationDate;
    private bool _isQuote;
    private int _agencyContactId;

    private IList<Action<ReservationBuilder>> _reservations =
        new List<Action<ReservationBuilder>>
        {
            _ => { },
        };

    public GroupReservationBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public GroupReservationBuilder WithReference(string? reference)
    {
        _reference = reference;
        return this;
    }

    public GroupReservationBuilder WithReservationDate(DateTime reservationDate)
    {
        _reservationDate = reservationDate;
        return this;
    }

    public GroupReservationBuilder WithIsQuote(bool isQuote)
    {
        _isQuote = isQuote;
        return this;
    }

    public GroupReservationBuilder WithAgencyContactId(int agencyContactId)
    {
        _agencyContactId = agencyContactId;
        return this;
    }

    public GroupReservationBuilder WithReservations(params Action<ReservationBuilder>[] reservationBuilders)
    {
        _reservations = reservationBuilders;
        return this;
    }

    public Core.Entities.Reservation.GroupReservation Build()
    {
        Core.Entities.Reservation.GroupReservation groupReservation = new()
        {
            Id = _id,
            Reference = _reference,
            ReservationDate = _reservationDate,
            IsQuote = _isQuote,
            AgencyContactId = _agencyContactId,
            AgencyContact = null!,
            Reservations = null!,
        };

        groupReservation.Reservations = _reservations.Select(action =>
            {
                var builder = new ReservationBuilder();
                action(builder);
                return builder.Build(groupReservation.Id, groupReservation);
            })
            .ToList();

        return groupReservation;
    }
}
