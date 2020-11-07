using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents;
using FluentAssertions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Domain.AggregateModels.PlayerRankingAggregate
{
    [TestFixture, Category("Unit")]
    public class PlayerLeaderBoardTests
    {
        private readonly PlayerId _player1 = new PlayerId(new Guid("BE0AE857-2BA0-4D84-A8C3-4D8D8832DA12"));
        private readonly PlayerId _player2 = new PlayerId(new Guid("18529E59-D0EE-4619-9930-0ECD407747D8"));
        private readonly PlayerId _player3 = new PlayerId(new Guid("BDC5785C-F29C-4E14-9A2A-195ED2F290C3"));
        private readonly PlayerId _player4 = new PlayerId(new Guid("5FB73311-1D68-4585-B25D-7AF1D32EEAB8"));
        private readonly PlayerId _player5 = new PlayerId(new Guid("25662DFE-D94A-4670-BECA-7BA9F73742E6"));
        private readonly PlayerId _player6 = new PlayerId(new Guid("6A04FB91-1B96-4C04-9E6A-1D851E7BDAF1"));

        #region AddPlayerToLeaderBoard

        [Test]
        public void AddPlayerToLeaderBoard_ShouldMovePlayerToFirstPlace_WhenNoPlayerIsInTheLeaderBoard()
        {
            var leaderBoard = new PlayerLeaderBoard(new List<PlayerOnLeaderBoard>());
            leaderBoard.AddPlayerToLeaderBoard(_player1, 1000);
            leaderBoard.AddPlayerToLeaderBoard(_player2, 990);

            leaderBoard.DomainEvents.Should().HaveCount(2);
            leaderBoard.VerifyPlayerAddedToLeaderBoard(_player1, 1);
            leaderBoard.VerifyPlayerAddedToLeaderBoard(_player2, 2);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
        }

        [Test]
        public void AddPlayerToLeaderBoard_ShouldMovePlayerToThirdPlace_WhenHisEloRatingIs91()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.AddPlayerToLeaderBoard(new PlayerId(_player6), 91);

            leaderBoard.DomainEvents.Should().HaveCount(4);
            leaderBoard.VerifyPlayerAddedToLeaderBoard(_player6, 3);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player3, 4,3,_player6);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player4, 5, 4, _player6);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player5, 6, 5, _player6);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player6).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(5);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(6);
        }

        [Test]
        public void AddPlayerToLeaderBoard_ShouldMovePlayerToFirstPlace_WhenHisEloRatingIs101()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.AddPlayerToLeaderBoard(new PlayerId(_player6), 101);

            leaderBoard.DomainEvents.Should().HaveCount(6);
            leaderBoard.VerifyPlayerAddedToLeaderBoard(_player6, 1);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player1, 2, 1, _player6);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player2, 3, 2, _player6);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player3, 4, 3, _player6);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player4, 5, 4, _player6);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player5, 6, 5, _player6);

            leaderBoard.Players.Single(x => x.PlayerId == _player6).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(5);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(6);
        }

        [Test]
        public void AddPlayerToLeaderBoard_ShouldMovePlayerToLastPlace_WhenHisEloRatingIs50()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.AddPlayerToLeaderBoard(new PlayerId(_player6), 50);

            leaderBoard.DomainEvents.Should().HaveCount(1);
            leaderBoard.VerifyPlayerAddedToLeaderBoard(_player6, 6);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
            leaderBoard.Players.Single(x => x.PlayerId == _player6).Rank.Should().Be(6);
        }

        #endregion

        #region UpdateLeaderBoard When Ascending Tests

        [Test]
        public void UpdateLeaderBoardAscend_ShouldStayInSamePlace_WhenPlayerEloDifferenceDoesNotExceedNextPlayer()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);
            leaderBoard.UpdateLeaderBoard(new PlayerId(_player2), 1);

            leaderBoard.DomainEvents.Should().HaveCount(0);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
        }

        [Test]
        public void UpdateLeaderBoardAscend_ShouldMoveUpTwoPlaces_WhenEloDifferencePutsPlayerAheadOfTwoPlayers()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);
            leaderBoard.UpdateLeaderBoard(new PlayerId(_player4), 11);

            leaderBoard.DomainEvents.Should().HaveCount(3);
            leaderBoard.VerifyPlayerAscendedOnLeaderBoard(_player4, 2, 4, null);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player2, 3, 2, _player4);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player3, 4, 3, _player4);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
        }

        [Test]
        public void UpdateLeaderBoardAscend_ShouldMoveUpOnePlace_WhenEloDifferencePutsPlayerAheadOfOnePlayers()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);
            leaderBoard.UpdateLeaderBoard(new PlayerId(_player4), 6);

            leaderBoard.DomainEvents.Should().HaveCount(2);
            leaderBoard.VerifyPlayerAscendedOnLeaderBoard(_player4, 3, 4, null);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player3, 4, 3, _player4);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
        }

        #endregion

        #region UpdateLeaderBoard When Descending Tests

        [Test]
        public void UpdateLeaderBoardDescend_ShouldStayInSamePlace_WhenPlayerEloDifferenceDoesNotExceedNextPlayer()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);
            leaderBoard.UpdateLeaderBoard(new PlayerId(_player2), -1);
            
            leaderBoard.DomainEvents.Should().HaveCount(0);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
        }

        [Test]
        public void UpdateLeaderBoardDescend_ShouldMoveDownTwoPlaces_WhenEloDifferencePutsPlayerDownTwoPlaces()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);
            leaderBoard.UpdateLeaderBoard(new PlayerId(_player2), -11);

            leaderBoard.DomainEvents.Should().HaveCount(3);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player2, 4, 2, null);
            leaderBoard.VerifyPlayerAscendedOnLeaderBoard(_player3, 2, 3, _player2);
            leaderBoard.VerifyPlayerAscendedOnLeaderBoard(_player4, 3, 4, _player2);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
        }

        [Test]
        public void UpdateLeaderBoardDescend_ShouldMoveDownOnePlace_WhenEloDifferencePutsPlayerDownOnePlace()
        {
            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(_player1, 100, 1),
                new PlayerOnLeaderBoard(_player2, 95, 2),
                new PlayerOnLeaderBoard(_player3, 90, 3),
                new PlayerOnLeaderBoard(_player4, 85, 4),
                new PlayerOnLeaderBoard(_player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);
            leaderBoard.UpdateLeaderBoard(new PlayerId(_player2), -6);

            leaderBoard.DomainEvents.Should().HaveCount(2);
            leaderBoard.VerifyPlayerDescendedOnLeaderBoard(_player2, 3, 2,null);
            leaderBoard.VerifyPlayerAscendedOnLeaderBoard(_player3, 2, 3, _player2);

            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
        }

        #endregion
    }

    internal static class PlayerLeaderBoardTestExtensions
    {
        internal static void VerifyPlayerAscendedOnLeaderBoard(this PlayerLeaderBoard self, PlayerId playerId, int currentRank, int previousRank, PlayerId descendingPlayerId)
        {
            var evt = self.DomainEvents.OfType<PlayerAscendedOnLeaderBoard>().Single(x => x.PlayerId == playerId);
            evt.CurrentRank.Should().Be(currentRank);
            evt.PreviousRank.Should().Be(previousRank);
            evt.DescendingPlayerId.Should().Be(descendingPlayerId);
        }
        internal static void VerifyPlayerDescendedOnLeaderBoard(this PlayerLeaderBoard self, PlayerId playerId, int currentRank, int previousRank, PlayerId ascendingPlayerId)
        {
            var evt = self.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == playerId);
            evt.CurrentRank.Should().Be(currentRank);
            evt.PreviousRank.Should().Be(previousRank);
            evt.AscendingPlayerId.Should().Be(ascendingPlayerId);
        }

        internal static void VerifyPlayerAddedToLeaderBoard(this PlayerLeaderBoard self, PlayerId playerId, int rank)
        {
            var evt = self.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Single(x => x.PlayerId == playerId);
            evt.Rank.Should().Be(rank);
        }
    }
}