using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;
using EloMatches.IntegrationEvents.SeedWork;

namespace EloMatches.Api.Application.IntegrationEvents
{
    public interface ITranslateDomainEventToIntegrationEvent<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        Task<IIntegrationEvent> Translate(TDomainEvent domainEvent);
    }
}