using PMS.Backend.Features.Exceptions;

namespace PMS.Backend.Features.Shared.ValueObjects;

internal record DateRange
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

    private DateRange() : this(DateOnly.MinValue, DateOnly.MaxValue)
    {
    }
}
