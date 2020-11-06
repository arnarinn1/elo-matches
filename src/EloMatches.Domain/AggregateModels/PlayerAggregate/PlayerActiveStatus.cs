using System;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate
{
    public class PlayerActiveStatus
    {
        public DateTime? ActiveSince { get; private set; }
        public DateTime? DeactivatedSince { get; private set; }

        public static PlayerActiveStatus Active()
        {
            return new PlayerActiveStatus
            {
                ActiveSince = SystemTime.Now(),
                DeactivatedSince = null
            };
        }

        public static PlayerActiveStatus Inactive()
        {
            return new PlayerActiveStatus
            {
                ActiveSince = null,
                DeactivatedSince = SystemTime.Now()
            };
        }

        private PlayerActiveStatus() { }
    }
}