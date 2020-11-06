using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Api.Features.Players.Commands.CreatePlayer.HttpPayloads;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : BaseCommandHandler<CreatePlayerCommand, CreatePlayerResponseBody>
    {
        private readonly IPlayerRepository _repository;

        public CreatePlayerCommandHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<(IReadOnlyCollection<IDomainEvent>, CreatePlayerResponseBody)> HandleCommand(CreatePlayerCommand command, CancellationToken cancellationToken = default)
        {
            var player = new Player(command.PlayerId, command.UserName, command.DisplayName, command.Email);
            
            await _repository.Add(player);

            return (player.DomainEvents, new CreatePlayerResponseBody(command.PlayerId));
        }
    }
}