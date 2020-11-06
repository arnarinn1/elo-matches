using System;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents
{
    public class PlayerDeactivated : DomainEvent
    {
        public PlayerDeactivated(PlayerId playerId, PlayerActiveStatus activeStatus, DateTime hadBeenActiveSince) : base(playerId, "Player")
        {
            PlayerId = playerId;
            ActiveStatus = activeStatus;
            HadBeenActiveSince = hadBeenActiveSince;
        }

        public PlayerId PlayerId { get; }
        public PlayerActiveStatus ActiveStatus { get; }
        public DateTime HadBeenActiveSince { get; }
    }
}