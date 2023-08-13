using MediatR;
using PMS.Backend.Domain.Common;
using PMS.Backend.Persistence.Db;

namespace PMS.Backend.Persistence.Extensions;

internal static class MediatorExtensions
{
    internal static async Task DispatchDomainEventsAsync(this IMediator mediator, PmsContext context)
    {
        List<Entity> domainEntities = context.ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .Where(entity => entity.DomainEvents?.Count != 0)
            .ToList();
        
        List<DomainEvent> events = domainEntities
            .SelectMany(entity => entity.DomainEvents!)
            .ToList();
        
        domainEntities.ForEach(entity => entity.ClearDomainEvents());
        
        foreach (DomainEvent domainEvent in events)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
