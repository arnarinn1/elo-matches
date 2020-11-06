using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Players.Commands.ChangePlayerNames
{
    public class ChangePlayerNamesCommandHandler : BaseCommandHandler<ChangePlayerNamesCommand, CommandHandlerResponse>
    {
        private readonly IPlayerRepository _playerRepository;

        public ChangePlayerNamesCommandHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerRepository playerRepository) : base(domainEventProcessors)
        {
            _playerRepository = playerRepository ?? throw new ArgumentNullException(nameof(playerRepository));
        }

        protected override async Task<(IReadOnlyCollection<IDomainEvent>, CommandHandlerResponse)> HandleCommand(ChangePlayerNamesCommand command, CancellationToken cancellationToken = default)
        {
            var player = await _playerRepository.GetOrThrowIfNotFound(command.PlayerId);
            
            player.ChangeUserName(command.UserName);
            player.ChangeDisplayName(command.DisplayName);

            return (player.DomainEvents, CommandHandlerResponse.NoResponse);
        }
    }
}