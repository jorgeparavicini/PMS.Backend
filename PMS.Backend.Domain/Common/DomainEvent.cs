using System;

namespace PMS.Backend.Domain.Common;

public abstract record DomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.Now;
}
