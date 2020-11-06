using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents;
using EloMatches.IntegrationEvents.Events.Players;
using EloMatches.IntegrationEvents.SeedWork;

namespace EloMatches.Api.Application.IntegrationEvents.Translations.Players
{
    public class PlayerCreatedTranslation : ITranslateDomainEventToIntegrationEvent<PlayerCreated>
    {
        public Task<IIntegrationEvent> Translate(PlayerCreated domainEvent)
        {
            return Task.FromResult(new PlayerCreatedIntegrationEvent(domainEvent.PlayerId) as IIntegrationEvent);
        }
    }
}