using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Players.Commands.ChangeEmail
{
    public class ChangePlayerEmailCommandHandler : BaseCommandHandler<ChangePlayerEmailCommand, CommandHandlerResponse>
    {
        private readonly IPlayerRepository _repository;

        public ChangePlayerEmailCommandHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<(IReadOnlyCollection<IDomainEvent>, CommandHandlerResponse)> HandleCommand(ChangePlayerEmailCommand command, CancellationToken cancellationToken = default)
        {
            var player = await _repository.GetOrThrowIfNotFound(command.PlayerId);

            player.ChangeEmail(command.Email);

            return (player.DomainEvents, CommandHandlerResponse.NoResponse);
        }
    }
}