namespace PMS.Backend.Features.Shared;

internal abstract record DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.Now;
}
