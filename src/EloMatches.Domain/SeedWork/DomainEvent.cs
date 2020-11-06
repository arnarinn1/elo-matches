using System;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.SeedWork
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(string aggregateId, string aggregateType)
        {
            AggregateId = aggregateId;
            AggregateType = aggregateType;
            OccurrenceTime = SystemTime.Now();
        }

        public string AggregateId { get; }
        public string AggregateType { get; }
        public DateTime OccurrenceTime { get; }
    }
}