namespace PMS.Backend.Features.Exceptions;

/// <summary>
/// An exception indicating that a request has invalid data or is malformed.
/// </summary>
public class BadRequestException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class using an empty
    /// message.
    /// </summary>
    public BadRequestException() : base(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BadRequestException"/> class using the passed
    /// message.
    /// </summary>
    /// <param name="message">The message for the base exception.</param>
    public BadRequestException(string message) : base(message)
    {
    }
}