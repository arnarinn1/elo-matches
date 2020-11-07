using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.Persistence;
using EloMatches.Infrastructure.Persistence.CrudEntities;

namespace EloMatches.Api.Infrastructure.DomainEventProcessors
{
    public class PersistDomainEventsOccurrenceProcessor : IDomainEventProcessor
    {
        private readonly EloMatchesCommandContext _context;

        public PersistDomainEventsOccurrenceProcessor(EloMatchesCommandContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Process(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            await _context.AddRangeAsync(domainEvents.Select(domainEvent => new DomainEventOccurrenceEntity(domainEvent, _context.TransactionId)));
        }
    }
}