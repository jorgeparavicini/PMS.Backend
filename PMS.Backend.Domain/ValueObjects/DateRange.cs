using System;
using PMS.Backend.Domain.Exceptions;

namespace PMS.Backend.Domain.ValueObjects;

public record DateRange
{
    public DateOnly StartDate { get; }
    public DateOnly EndDate { get; }

    public DateRange(DateOnly startDate, DateOnly endDate)
    {
        if (startDate > endDate)
        {
            throw new InvalidDateRangeException(startDate, endDate);
        }

        StartDate = startDate;
        EndDate = endDate;
    }
}
