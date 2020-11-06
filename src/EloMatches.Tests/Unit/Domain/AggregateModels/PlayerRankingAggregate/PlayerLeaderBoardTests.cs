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

        [Test]
        public void AddPlayerToLeaderBoard_IsAddedToLeaderBoard()
        {
            var player1 = Guid.NewGuid();
            var player2 = Guid.NewGuid();
            var player3 = Guid.NewGuid();
            var player4 = Guid.NewGuid();
            var player5 = Guid.NewGuid();
            var player6 = Guid.NewGuid();

            var players = new List<PlayerOnLeaderBoard>
            {
                new PlayerOnLeaderBoard(player1, 100, 1),
                new PlayerOnLeaderBoard(player2, 95, 2),
                new PlayerOnLeaderBoard(player3, 90, 3),
                new PlayerOnLeaderBoard(player4, 85, 4),
                new PlayerOnLeaderBoard(player5, 80, 5)
            };

            var leaderBoard = new PlayerLeaderBoard(players);

            leaderBoard.AddPlayerToLeaderBoard(new PlayerId(player6), 91);

            leaderBoard.DomainEvents.Should().HaveCount(4);

            leaderBoard.Players.Single(x => x.PlayerId == player1).Rank.Should().Be(1);
            leaderBoard.Players.Single(x => x.PlayerId == player2).Rank.Should().Be(2);
            leaderBoard.Players.Single(x => x.PlayerId == player6).Rank.Should().Be(3);
            leaderBoard.Players.Single(x => x.PlayerId == player3).Rank.Should().Be(4);
            leaderBoard.Players.Single(x => x.PlayerId == player4).Rank.Should().Be(5);
            leaderBoard.Players.Single(x => x.PlayerId == player5).Rank.Should().Be(6);
        }
    }
}