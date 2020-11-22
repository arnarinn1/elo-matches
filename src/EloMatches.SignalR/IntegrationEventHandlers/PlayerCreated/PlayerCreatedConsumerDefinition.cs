using MassTransit.Definition;

namespace EloMatches.SignalR.IntegrationEventHandlers.PlayerCreated
{
    public class PlayerCreatedConsumerDefinition : ConsumerDefinition<PlayerCreatedConsumer>
    {
        public PlayerCreatedConsumerDefinition()
        {
            EndpointName = "elo-matches_notification-hub";
        }
    }
}