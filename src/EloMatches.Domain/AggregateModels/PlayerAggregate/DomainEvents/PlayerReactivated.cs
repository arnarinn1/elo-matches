using System;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents
{
    public class PlayerReactivated : DomainEvent
    {
        public PlayerReactivated(PlayerId playerId, PlayerActiveStatus activeStatus, DateTime hadBeenDeactivatedSince) : base(playerId, "Player")
        {
            PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
            ActiveStatus = activeStatus ?? throw new ArgumentNullException(nameof(activeStatus));
            HadBeenDeactivatedSince = hadBeenDeactivatedSince;
        }

        public PlayerId PlayerId { get; }
        public PlayerActiveStatus ActiveStatus { get; }
        public DateTime HadBeenDeactivatedSince { get; }
    }
}