using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents
{
    public class PlayerDescendedOnLeaderBoard : DomainEvent
    {
        public PlayerDescendedOnLeaderBoard(PlayerId playerId, int previousRank, int currentRank) : base(playerId.ToString(), "PlayerLeaderBoard")
        {
            PlayerId = playerId;
            PreviousRank = previousRank;
            CurrentRank = currentRank;
        }

        public PlayerId PlayerId { get; }
        public int PreviousRank { get; }
        public int CurrentRank { get; }
    }
}