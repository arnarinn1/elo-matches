using System;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents
{
    public class PlayerCreated : DomainEvent
    {
        public PlayerCreated(PlayerId playerId, Name userName, Name displayName, EmailAddress email) : base(playerId, "Player")
        {
            PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            Email = email ?? throw new ArgumentNullException(nameof(displayName));
        }

        public PlayerId PlayerId { get; }
        public Name UserName { get; }
        public Name DisplayName { get; }
        public EmailAddress Email { get; }
    }
}