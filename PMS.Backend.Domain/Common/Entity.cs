using System;
using System.Collections.Generic;

namespace PMS.Backend.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; } = Guid.NewGuid();

    public bool IsDeleted { get; private set; }

    private List<DomainEvent>? _domainEvents;
    public IReadOnlyCollection<DomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();
    
    public byte[] RowVersion { get; init; } = Array.Empty<byte>();

    protected void AddDomainEvent(DomainEvent @event)
    {
        _domainEvents ??= new List<DomainEvent>();
        _domainEvents.Add(@event);
    }

    public void RemoveDomainEvent(DomainEvent @event)
    {
        _domainEvents?.Remove(@event);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public virtual void Delete()
    {
        IsDeleted = true;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity item)
        {
            return false;
        }

        if (ReferenceEquals(this, item))
        {
            return true;
        }

        if (GetType() != item.GetType())
        {
            return false;
        }

        return item.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() ^ 31;
    }
}
