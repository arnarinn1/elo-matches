using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents
{
    public class PlayerAscendedOnLeaderBoard : DomainEvent
    {
        public PlayerAscendedOnLeaderBoard(PlayerId playerId, PlayerId descendingPlayerId, int previousRank, int currentRank) : base(playerId.ToString(), "PlayerLeaderBoard")
        {
            PlayerId = playerId;
            DescendingPlayerId = descendingPlayerId;
            PreviousRank = previousRank;
            CurrentRank = currentRank;
        }

        public PlayerId PlayerId { get; }
        public PlayerId DescendingPlayerId { get; }
        public int PreviousRank { get; }
        public int CurrentRank { get; }
    }
}