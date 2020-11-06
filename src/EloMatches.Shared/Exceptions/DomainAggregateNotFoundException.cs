using System;

namespace EloMatches.Shared.Exceptions
{
    public class DomainAggregateNotFoundException : Exception
    {
        public DomainAggregateNotFoundException(string aggregateId, string aggregateType) : base($"Aggregate of type = '{aggregateType}' with aggregateId = '{aggregateId}' was not found")
        {
            AggregateId = aggregateId;
            AggregateType = aggregateType;
        }

        public string AggregateId { get; set; }
        public string AggregateType { get; set; }
    }
}