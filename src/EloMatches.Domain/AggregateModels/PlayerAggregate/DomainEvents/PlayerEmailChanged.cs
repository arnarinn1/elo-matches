using System;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents
{
    public class PlayerEmailChanged : DomainEvent
    {
        public PlayerEmailChanged(PlayerId playerId, EmailAddress email, EmailAddress previousEmail) : base(playerId, "Player")
        {
            PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PreviousEmail = previousEmail ?? throw new ArgumentNullException(nameof(previousEmail));
        }

        public PlayerId PlayerId { get; }
        public EmailAddress Email { get; }
        public EmailAddress PreviousEmail { get; }
    }
}