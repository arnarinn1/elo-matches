using System;
using EloMatches.IntegrationEvents.SeedWork;

namespace EloMatches.IntegrationEvents.Events.Players
{
    public class PlayerCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid PlayerId { get; set; }

        public PlayerCreatedIntegrationEvent(Guid playerId)
        {
            PlayerId = playerId;
        }

        public PlayerCreatedIntegrationEvent() { }
    }
}