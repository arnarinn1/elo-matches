using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class MatchStreakTests
    {
        [Test]
        public void Construction_ShouldBeEquivalent_WhenBothStatisticsAreTheSame()
        {
            var left = new MatchStreak();
            var right = new MatchStreak();
            (left == right).Should().BeTrue();
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEquivalent_WhenStatisticsAreNotTheSame()
        {
            var left = new MatchStreak();
            var right = new MatchStreak().AddWin();

            (left != right).Should().BeTrue();
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            MatchStreak left = null;
            var right = new MatchStreak();
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new MatchStreak();
            MatchStreak right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldBeEqual_WhenBothSidesAreNull()
        {
            MatchStreak left = null;
            MatchStreak right = null;
            (left == right).Should().BeTrue();
        }

        [Test]
        public void AddWin_ShouldBeOnWinningStreakWithTwoWins_WhenTwoWinsAreAdded()
        {
            var streak = new MatchStreak();
            streak = streak.AddWin();
            streak = streak.AddWin();

            streak.Type.Should().Be(MatchStreakType.Winning);
            streak.StreakCount.Should().Be(2);
        }

        [Test]
        public void AddLoss_ShouldBeOnLosingStreakWithTwoLosses_WhenTwoLossesAreAdded()
        {
            var streak = new MatchStreak();
            streak = streak.AddLoss();
            streak = streak.AddLoss();

            streak.Type.Should().Be(MatchStreakType.Losing);
            streak.StreakCount.Should().Be(2);
        }

        [Test]
        public void AddLoss_ShouldBeOnLosingStreakWithOneLoss_WhenLossIsAddedAfterHavingBeenOnThreeGameWinningStreak()
        {
            var streak = new MatchStreak().AddWin().AddWin().AddWin();
            streak = streak.AddLoss();
            streak.Type.Should().Be(MatchStreakType.Losing);
            streak.StreakCount.Should().Be(1);
        }
    }
}