namespace PMS.Backend.Features.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(): base(string.Empty) {}
    
    public NotFoundException(string message): base(message) {}
    
}