using System;
using PMS.Backend.Core.Entities.Reservation;

namespace PMS.Backend.Test.Builders.Reservation.Entity;

public class ReservationDetailBuilder
{
    private int _id;
    private DateTime _reservationDate;
    private DateOnly _checkIn = DateOnly.FromDateTime(DateTime.Today);
    private DateOnly _checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    private DateTime? _folioClosedOn;

    public ReservationDetailBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public ReservationDetailBuilder WithReservationDate(DateTime reservationDate)
    {
        _reservationDate = reservationDate;
        return this;
    }

    public ReservationDetailBuilder WithCheckIn(DateOnly checkIn)
    {
        _checkIn = checkIn;
        return this;
    }

    public ReservationDetailBuilder WithCheckOut(DateOnly checkOut)
    {
        _checkOut = checkOut;
        return this;
    }

    public ReservationDetailBuilder WithFolioClosedOn(DateTime? folioClosedOn)
    {
        _folioClosedOn = folioClosedOn;
        return this;
    }

    public ReservationDetail Build(int reservationId, Core.Entities.Reservation.Reservation reservation) => new()
    {
        Id = _id,
        ReservationDate = _reservationDate,
        CheckIn = _checkIn,
        CheckOut = _checkOut,
        FolioClosedOn = _folioClosedOn,
        ReservationId = reservationId,
        Reservation = reservation,
    };
}
