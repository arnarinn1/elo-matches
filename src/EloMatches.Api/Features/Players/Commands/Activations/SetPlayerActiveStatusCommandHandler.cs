using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Players.Commands.Activations
{
    public class SetPlayerActiveStatusCommandHandler : BaseCommandHandler<SetPlayerActiveStatusCommand, CommandHandlerResponse>
    {
        private readonly IPlayerRepository _repository;

        public SetPlayerActiveStatusCommandHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<(IReadOnlyCollection<IDomainEvent>, CommandHandlerResponse)> HandleCommand(SetPlayerActiveStatusCommand command, CancellationToken cancellationToken = default)
        {
            var player = await _repository.GetOrThrowIfNotFound(command.PlayerId);

            if (command.ActiveStatus)
                player.Reactivate();
            else 
                player.Deactivate();

            return (player.DomainEvents, CommandHandlerResponse.NoResponse);
        }
    }
}