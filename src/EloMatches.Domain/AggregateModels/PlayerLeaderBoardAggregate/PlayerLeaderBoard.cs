using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate
{
    public class PlayerLeaderBoard : Aggregate, IAggregateRoot
    {
        public ICollection<PlayerOnLeaderBoard> Players { get; set; } //todo -> private

        public PlayerLeaderBoard(ICollection<PlayerOnLeaderBoard> players)
        {
            Players = players;
        }

        public void UpdateLeaderBoard(PlayerId playerId, decimal eloDifferenceForLastGame)
        {
            var player = Players.Single(x => x.PlayerId == playerId.Id);

            if (eloDifferenceForLastGame > 0)
                Ascend(player, eloDifferenceForLastGame);
            else 
                Descend(player, eloDifferenceForLastGame);

            EnsureThatLeaderBoardIsInSequentialOrder();
        }

        public void AddPlayerToLeaderBoard(PlayerId playerId, decimal eloRating)
        {
            if (Players.Any(x => x.PlayerId == playerId.Id))
                return;

            var currentRank = Players.OrderByDescending(x => x.EloRating).Count(x => x.EloRating > eloRating) + 1;

            Players.Add(new PlayerOnLeaderBoard(playerId, eloRating, currentRank));

            AddDomainEvent(new PlayerAddedToLeaderBoard(playerId, currentRank));

            foreach (var playerToDescend in Players.Where(x => x.EloRating < eloRating))
            {
                AddDomainEvent(new PlayerDescendedOnLeaderBoard(new PlayerId(playerToDescend.PlayerId), playerToDescend.Rank, ++playerToDescend.Rank));
            }

            EnsureThatLeaderBoardIsInSequentialOrder();
        }

        private void Ascend(PlayerOnLeaderBoard player, decimal eloDifferenceForLastGame)
        {
            var currentRating = player.EloRating;
            var updatedRating = currentRating + eloDifferenceForLastGame;

            var currentRank = player.Rank;
            var updatedRank = Players.OrderByDescending(x => x.EloRating).Count(x => x.EloRating > updatedRating) + 1;

            player.EloRating = updatedRating;

            if (currentRank == updatedRank)
                return;

            player.Rank = updatedRank;

            AddDomainEvent(new PlayerAscendedOnLeaderBoard(new PlayerId(player.PlayerId), currentRank, updatedRank));

            foreach (var playerToDescend in Players.Where(x => x.EloRating < updatedRating && x.EloRating > currentRating))
            {
                AddDomainEvent(new PlayerDescendedOnLeaderBoard(new PlayerId(player.PlayerId), playerToDescend.Rank, ++playerToDescend.Rank));
            }
        }

        private void Descend(PlayerOnLeaderBoard player, decimal eloDifferenceForLastGame)
        {
            var currentRating = player.EloRating;
            var updatedRating = currentRating + eloDifferenceForLastGame;

            var currentRank = player.Rank;
            var updatedRank = Players.OrderByDescending(x => x.EloRating).Count(x => x.EloRating > updatedRating);

            player.EloRating = updatedRating;

            if (currentRank == updatedRank)
                return;

            player.Rank = updatedRank;

            AddDomainEvent(new PlayerDescendedOnLeaderBoard(new PlayerId(player.PlayerId), currentRank, updatedRank));

            foreach (var playerToAscend in Players.Where(x => x.EloRating > updatedRating && x.EloRating < currentRating))
            {
                AddDomainEvent(new PlayerAscendedOnLeaderBoard(new PlayerId(playerToAscend.PlayerId), playerToAscend.Rank, --playerToAscend.Rank));
            }
        }

        private void EnsureThatLeaderBoardIsInSequentialOrder()
        {
            var currentRank = 1;

            if (Players.OrderBy(x => x.Rank).Any(player => player.Rank != currentRank++))
                throw new Exception("Ranking on PlayerLeaderBoard is not sequential order");
        }
    }
}