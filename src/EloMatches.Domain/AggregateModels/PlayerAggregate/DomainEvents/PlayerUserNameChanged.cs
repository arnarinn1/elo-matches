using System;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents
{
    public class PlayerUserNameChanged : DomainEvent
    {
        public PlayerUserNameChanged(PlayerId playerId, Name userName, Name previousUserName) : base(playerId, "Player")
        {
            PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            PreviousUserName = previousUserName ?? throw new ArgumentNullException(nameof(previousUserName));
        }

        public PlayerId PlayerId { get; }
        public Name UserName { get; }
        public Name PreviousUserName { get; }
    }
}