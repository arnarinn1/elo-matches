using System;

namespace EloMatches.Query.Projections.DomainEventOccurrences
{
    public class DomainEventOccurrenceProjection
    {
        public int Id { get; set; }
        public string AggregateId { get; set; }
        public string AggregateType { get; set; }
        public string TypeName { get; set; }
        public string SerializedData { get; set; }
        public DateTime OccurrenceDate { get; set; }
        public Guid? TransactionId { get; set; }
    }
}