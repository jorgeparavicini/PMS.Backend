namespace PMS.Backend.Features.Exceptions;

public class NotFoundException<T> : Exception
{
    public NotFoundException(Guid id) :
        base($"{nameof(T)} not found with id '{id}")
    {
    }
}
