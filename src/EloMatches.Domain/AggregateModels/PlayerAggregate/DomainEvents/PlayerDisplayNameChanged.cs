using System;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents
{
    public class PlayerDisplayNameChanged : DomainEvent
    {
        public PlayerDisplayNameChanged(PlayerId playerId, Name displayName, Name previousDisplayName) : base(playerId, "Player")
        {
            PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            PreviousDisplayName = previousDisplayName ?? throw new ArgumentNullException(nameof(previousDisplayName));
        }

        public PlayerId PlayerId { get; }
        public Name DisplayName { get; }
        public Name PreviousDisplayName { get; }
    }
}