using MediatR;

namespace EloMatches.Domain.SeedWork
{
    public interface IDomainEventHandler<in TDomainEvent>  : INotificationHandler<TDomainEvent> 
        where TDomainEvent : IDomainEvent { }
}