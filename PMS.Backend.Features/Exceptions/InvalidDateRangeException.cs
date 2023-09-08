namespace PMS.Backend.Features.Exceptions;

public class InvalidDateRangeException : Exception
{
    public InvalidDateRangeException(DateOnly from, DateOnly to)
        : base($"Invalid date range, date start '{from}' must come before the date end '{to}'.")
    {
    }
}
