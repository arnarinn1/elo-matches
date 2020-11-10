using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.Domain.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class NumberOfMatchesStatisticsTests
    {
        [Test]
        public void Construction_ShouldBeEquivalent_WhenBothStatisticsAreTheSame()
        {
            var left = new NumberOfMatchesStatistics();
            var right = new NumberOfMatchesStatistics();
            (left == right).Should().BeTrue();
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEquivalent_WhenStatisticsAreNotTheSame()
        {
            var left = new NumberOfMatchesStatistics();
            var right = new NumberOfMatchesStatistics().AddWin();
            
            (left != right).Should().BeTrue();
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            NumberOfMatchesStatistics left = null;
            var right = new NumberOfMatchesStatistics();
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new NumberOfMatchesStatistics();
            NumberOfMatchesStatistics right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldBeEqual_WhenBothSidesAreNull()
        {
            NumberOfMatchesStatistics left = null;
            NumberOfMatchesStatistics right = null;
            (left == right).Should().BeTrue();
        }

        [Test]
        public void AddWin_ShouldHaveTwoWins_WhenTwoWinsAreAdded()
        {
            var statistics = new NumberOfMatchesStatistics();
            statistics = statistics.AddWin();
            statistics = statistics.AddWin();

            statistics.PlayedGames.Should().Be(2);
            statistics.Wins.Should().Be(2);
            statistics.Losses.Should().Be(0);
            statistics.Differential.Should().Be(2);
            statistics.WinPercentage.Should().Be(1.0m);
        }

        [Test]
        public void AddWinAndAddLoss_ShouldHaveOneWinAndOneLoss_WhenOneWinAndOneLossIsAdded()
        {
            var statistics = new NumberOfMatchesStatistics();
            statistics = statistics.AddWin();
            statistics = statistics.AddLoss();

            statistics.PlayedGames.Should().Be(2);
            statistics.Wins.Should().Be(1);
            statistics.Losses.Should().Be(1);
            statistics.Differential.Should().Be(0);
            statistics.WinPercentage.Should().Be(0.5m);
        }

        [Test]
        public void AddLoss_ShouldHaveTwoLosses_WhenTwoLossesAreAdded()
        {
            var statistics = new NumberOfMatchesStatistics();
            statistics = statistics.AddLoss();
            statistics = statistics.AddLoss();

            statistics.PlayedGames.Should().Be(2);
            statistics.Wins.Should().Be(0);
            statistics.Losses.Should().Be(2);
            statistics.Differential.Should().Be(-2);
            statistics.WinPercentage.Should().Be(0.0m);
        }
    }
}