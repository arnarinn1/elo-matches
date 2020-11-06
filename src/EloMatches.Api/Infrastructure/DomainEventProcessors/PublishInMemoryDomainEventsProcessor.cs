using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;
using MediatR;

namespace EloMatches.Api.Infrastructure.DomainEventProcessors
{
    public class PublishInMemoryDomainEventsProcessor : IDomainEventProcessor
    {
        private readonly IMediator _mediator;

        public PublishInMemoryDomainEventsProcessor(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Process(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
                await _mediator.Publish(domainEvent);
        }
    }
}