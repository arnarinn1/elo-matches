﻿using System;
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

            leaderBoard.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Single(x => x.PlayerId == _player1).CurrentRank.Should().Be(1);
            leaderBoard.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Single(x => x.PlayerId == _player2).CurrentRank.Should().Be(2);

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

            leaderBoard.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Single().CurrentRank.Should().Be(3);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player3).CurrentRank.Should().Be(4);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player4).CurrentRank.Should().Be(5);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player5).CurrentRank.Should().Be(6);

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

            leaderBoard.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Single().CurrentRank.Should().Be(1);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player1).CurrentRank.Should().Be(2);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player2).CurrentRank.Should().Be(3);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player3).CurrentRank.Should().Be(4);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player4).CurrentRank.Should().Be(5);
            leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single(x => x.PlayerId == _player5).CurrentRank.Should().Be(6);

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

            leaderBoard.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Single().CurrentRank.Should().Be(6);
            
            leaderBoard.Players.Single(x => x.PlayerId == _player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == _player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == _player3).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == _player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == _player5).Rank.Should().Be(5);
            leaderBoard.Players.Single(x => x.PlayerId == _player6).Rank.Should().Be(6);
        }

        #endregion

        #region UpdateLeaderBoard Tests

        [Test]
        public void UpdateLeaderBoard_PlayerIsUpTwoRanks()
        {
            var player1 = Guid.NewGuid();
            var player2 = Guid.NewGuid();
            var player3 = Guid.NewGuid();
            var player4 = Guid.NewGuid();
            var player5 = Guid.NewGuid();

            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(player1, 100, 1),
                new PlayerOnLeaderBoard(player2, 95, 2),
                new PlayerOnLeaderBoard(player3, 90, 3),
                new PlayerOnLeaderBoard(player4, 85, 4),
                new PlayerOnLeaderBoard(player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.UpdateLeaderBoard(new PlayerId(player4), 11);

            leaderBoard.DomainEvents.Should().HaveCount(3);

            leaderBoard.Players.Single(x => x.PlayerId == player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == player4).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == player2).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == player3).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == player5).Rank.Should().Be(5);

            var player4Event = leaderBoard.DomainEvents.OfType<PlayerAscendedOnLeaderBoard>().Single();
            player4Event.CurrentRank.Should().Be(2);
            player4Event.PreviousRank.Should().Be(4);
        }

        [Test]
        public void UpdateLeaderBoard_PlayerIsDownTwoRanks()
        {
            var player1 = Guid.NewGuid();
            var player2 = Guid.NewGuid();
            var player3 = Guid.NewGuid();
            var player4 = Guid.NewGuid();
            var player5 = Guid.NewGuid();

            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(player1, 100, 1),
                new PlayerOnLeaderBoard(player2, 95, 2),
                new PlayerOnLeaderBoard(player3, 90, 3),
                new PlayerOnLeaderBoard(player4, 85, 4),
                new PlayerOnLeaderBoard(player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.UpdateLeaderBoard(new PlayerId(player2), -11);

            leaderBoard.DomainEvents.Should().HaveCount(3);

            leaderBoard.Players.Single(x => x.PlayerId == player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == player3).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == player4).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == player2).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == player5).Rank.Should().Be(5);

            var player2Event = leaderBoard.DomainEvents.OfType<PlayerDescendedOnLeaderBoard>().Single();
            player2Event.CurrentRank.Should().Be(4);
            player2Event.PreviousRank.Should().Be(2);
        }

        [Test]
        public void UpdateLeaderBoard_PlayerGainsPointsButStaysInSameRank()
        {
            var player1 = Guid.NewGuid();
            var player2 = Guid.NewGuid();
            var player3 = Guid.NewGuid();
            var player4 = Guid.NewGuid();
            var player5 = Guid.NewGuid();

            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(player1, 100, 1),
                new PlayerOnLeaderBoard(player2, 95, 2),
                new PlayerOnLeaderBoard(player3, 90, 3),
                new PlayerOnLeaderBoard(player4, 85, 4),
                new PlayerOnLeaderBoard(player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.UpdateLeaderBoard(new PlayerId(player2), 1);

            leaderBoard.DomainEvents.Should().HaveCount(0);

            leaderBoard.Players.Single(x => x.PlayerId == player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == player3).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == player5).Rank.Should().Be(5);
        }

        [Test]
        public void UpdateLeaderBoard_PlayerLosesPointsButStaysInSameRank()
        {
            var player1 = Guid.NewGuid();
            var player2 = Guid.NewGuid();
            var player3 = Guid.NewGuid();
            var player4 = Guid.NewGuid();
            var player5 = Guid.NewGuid();

            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(player1, 100, 1),
                new PlayerOnLeaderBoard(player2, 95, 2),
                new PlayerOnLeaderBoard(player3, 90, 3),
                new PlayerOnLeaderBoard(player4, 85, 4),
                new PlayerOnLeaderBoard(player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.UpdateLeaderBoard(new PlayerId(player2), -1);

            leaderBoard.DomainEvents.Should().HaveCount(0);

            leaderBoard.Players.Single(x => x.PlayerId == player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == player3).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == player4).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == player5).Rank.Should().Be(5);
        }

        #endregion
    }
}