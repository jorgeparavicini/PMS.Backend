using System;
using PMS.Backend.Features.GraphQL.Reservation.Models.Input;

namespace PMS.Backend.Test.Builders.Reservation.Models;

public class CreateReservationReservationDetailsInputBuilder
{
    private DateOnly _checkIn = DateOnly.FromDateTime(DateTime.Today);
    private DateOnly _checkOut = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

    public CreateReservationReservationDetailsInputBuilder WithCheckIn(DateOnly checkIn)
    {
        _checkIn = checkIn;
        return this;
    }

    public CreateReservationReservationDetailsInputBuilder WithCheckOut(DateOnly checkOut)
    {
        _checkOut = checkOut;
        return this;
    }

    public CreateReservationReservationDetailsInput Build() => new()
    {
        CheckIn = _checkIn,
        CheckOut = _checkOut,
    };
}
