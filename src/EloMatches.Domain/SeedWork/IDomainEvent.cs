using System;
using MediatR;

namespace EloMatches.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        public string AggregateId { get; }
        public string AggregateType { get; }
        public DateTime OccurrenceTime { get; }
    }
}