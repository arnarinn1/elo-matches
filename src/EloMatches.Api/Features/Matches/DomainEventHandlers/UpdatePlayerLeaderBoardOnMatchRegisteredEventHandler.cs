using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.MatchesAggregate;
using EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Matches.DomainEventHandlers
{
    public class UpdatePlayerLeaderBoardOnMatchRegisteredEventHandler : BaseEventHandler<MatchRegistered>
    {
        private readonly IPlayerLeaderBoardRepository _repository;

        public UpdatePlayerLeaderBoardOnMatchRegisteredEventHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IPlayerLeaderBoardRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> HandleEvent(MatchRegistered domainEvent, CancellationToken cancellationToken = default)
        {
            var leaderBoard = await _repository.Get();

            ReactToEvent(leaderBoard, domainEvent.Winner);
            ReactToEvent(leaderBoard, domainEvent.Loser);
            
            await _repository.Process(leaderBoard);

            return leaderBoard.DomainEvents;
        }

        private static void ReactToEvent(PlayerLeaderBoard leaderBoard, PlayerMatchResult player)
        {
            var playerId = new PlayerId(player.PlayerId);

            if (player.GameNumber == 1)
                leaderBoard.AddPlayerToLeaderBoard(playerId, player.TotalEloRatingAfterGame);
            else
                leaderBoard.UpdateLeaderBoard(playerId, player.TotalEloRatingAfterGame);
        }
    }
}