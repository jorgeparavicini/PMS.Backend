namespace PMS.Backend.Features.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message): base(message) {}
    
}