using System;
using System.Collections.Generic;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate
{
    public class PlayerId : ValueObject
    {
        public Guid Id { get; private set; }

        public PlayerId(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid for PlayerId can't be empty");

            Id = id;
        }

        private PlayerId() {}

        public static implicit operator Guid(PlayerId playerId) => playerId.Id;
        public static implicit operator string(PlayerId playerId) => playerId.Id.ToString();
        
        public override string ToString() => Id.ToString();

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}