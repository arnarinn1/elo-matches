using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents
{
    public class PlayerAddedToLeaderBoard : DomainEvent
    {
        public PlayerAddedToLeaderBoard(PlayerId playerId, int currentRank) : base(playerId, "PlayerLeaderBoard")
        {
            PlayerId = playerId;
            CurrentRank = currentRank;
        }

        public PlayerId PlayerId { get; }
        public int CurrentRank { get; }
    }
}