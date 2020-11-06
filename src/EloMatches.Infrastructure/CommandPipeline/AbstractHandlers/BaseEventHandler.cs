using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;

// ReSharper disable StaticMemberInGenericType

namespace EloMatches.Infrastructure.CommandPipeline.AbstractHandlers
{
    public abstract class BaseEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly IEnumerable<IDomainEventProcessor> _domainEventProcessors;

        protected static IReadOnlyCollection<IDomainEvent> NoChanges = new IDomainEvent[0];

        protected BaseEventHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors)
        {
            _domainEventProcessors = domainEventProcessors ?? throw new ArgumentNullException(nameof(domainEventProcessors));
        }

        protected abstract Task<IReadOnlyCollection<IDomainEvent>> HandleEvent(TDomainEvent domainEvent, CancellationToken cancellationToken = default);

        public async Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var domainEvents= await HandleEvent(domainEvent, cancellationToken);

            foreach (var domainEventProcessor in _domainEventProcessors)
                await domainEventProcessor.Process(domainEvents);
        }
    }
}