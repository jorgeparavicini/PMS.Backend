namespace PMS.Backend.Features.Exceptions;

/// <summary>
/// An exception indicating that a request was not able to be performed because an entity was
/// not found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of <see cref="NotFoundException"/> class using an empty message.
    /// </summary>
    public NotFoundException() : base(string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class using the passed
    /// message.
    /// </summary>
    /// <param name="message">The message for the base exception.</param>
    public NotFoundException(string message) : base(message)
    {
    }
}
