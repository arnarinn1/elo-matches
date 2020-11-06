﻿using System;
using System.Text.Json;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Infrastructure.Persistence.CrudEntities
{
    public class DomainEventOccurrenceEntity
    {
        public DomainEventOccurrenceEntity(IDomainEvent domainEvent)
        {
            AggregateId = domainEvent.AggregateId;
            AggregateType = domainEvent.AggregateType;
            TypeName = domainEvent.GetType().Name;
            SerializedData = JsonSerializer.Serialize(domainEvent, domainEvent.GetType());
            OccurrenceDate = domainEvent.OccurrenceTime;
        }

        private DomainEventOccurrenceEntity() {}
        
        public int Id { get; private set; }
        public string AggregateId { get; set; }
        public string AggregateType { get; set; }
        public string TypeName { get; set; }
        public string SerializedData { get; set; }
        public DateTime OccurrenceDate { get; set; }
    }
}