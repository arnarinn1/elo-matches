using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerRankingAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Players.DomainEventHandlers
{
    public class CreatePlayerRankingOnPlayerCreatedEventHandler : BaseEventHandler<PlayerCreated>
    {
        private readonly IPlayerRankingRepository _repository;

        public CreatePlayerRankingOnPlayerCreatedEventHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerRankingRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> HandleEvent(PlayerCreated domainEvent, CancellationToken cancellationToken = default)
        {
            if (await _repository.Get(domainEvent.PlayerId) is not null)
                return NoChanges;

            var playerRanking = new PlayerRanking(domainEvent.PlayerId);
            await _repository.Add(playerRanking);

            return playerRanking.DomainEvents;
        }
    }
}