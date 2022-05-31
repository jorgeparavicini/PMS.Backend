namespace PMS.Backend.Features.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException() : base(string.Empty)
    {
    }

    public BadRequestException(string message) : base(message)
    {
    }
}