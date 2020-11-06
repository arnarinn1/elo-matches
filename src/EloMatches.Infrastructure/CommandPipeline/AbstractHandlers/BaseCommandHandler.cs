using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Infrastructure.CommandPipeline.AbstractHandlers
{
    public abstract class BaseCommandHandler<TCommand, TResponse> : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        private readonly IEnumerable<IDomainEventProcessor> _domainEventProcessors;

        protected BaseCommandHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors)
        {
            _domainEventProcessors = domainEventProcessors ?? throw new ArgumentNullException(nameof(domainEventProcessors));
        }

        protected abstract Task<(IReadOnlyCollection<IDomainEvent>, TResponse)> HandleCommand(TCommand command, CancellationToken cancellationToken = default);

        public async Task<TResponse> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var (domainEvents, response) = await HandleCommand(request, cancellationToken);

            foreach (var domainEventProcessor in _domainEventProcessors)
                await domainEventProcessor.Process(domainEvents);

            return response;
        }
    }
}