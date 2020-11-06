using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerRankingAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Matches.DomainEventHandlers
{
    public class UpdatePlayerRankingOnMatchRegisteredEventHandler : BaseEventHandler<MatchRegistered>
    {
        private readonly IPlayerRankingRepository _repository;

        public UpdatePlayerRankingOnMatchRegisteredEventHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerRankingRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> HandleEvent(MatchRegistered domainEvent, CancellationToken cancellationToken = default)
        {
            var winnerPlayerRanking = await _repository.GetOrThrowIfNotFound(new PlayerId(domainEvent.Winner.PlayerId));
            var loserPlayerRanking = await _repository.GetOrThrowIfNotFound(new PlayerId(domainEvent.Loser.PlayerId));

            winnerPlayerRanking.AddWin(domainEvent.MatchResult, domainEvent.MatchDate, domainEvent.EloCalculationDifference);
            loserPlayerRanking.AddLoss(domainEvent.MatchResult, domainEvent.MatchDate, domainEvent.EloCalculationDifference);

            var domainEvents = winnerPlayerRanking.DomainEvents.ToList();
            domainEvents.AddRange(loserPlayerRanking.DomainEvents);

            return domainEvents;
        }
    }
}