using System;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class MatchResultTests
    {
        [Test]
        public void Construction_ShouldThrowArgumentException_WhenLoserScoreIsLessThanZero()
        {
            Action action = () => new MatchResult(1, -1);
            action.Should().Throw<ArgumentException>().Which.Message.Should().BeEquivalentTo("LoserScore can't be less than zero (Parameter 'loserScore')");
        }

        [Test]
        public void Construction_ShouldThrowArgumentException_WhenWinnerAndLoserScoreAreZero()
        {
            Action action = () => new MatchResult(0, 0);
            action.Should().Throw<ArgumentException>().Which.Message.Should().BeEquivalentTo("WinnerScore must be greater than LoserScore");
        }

        [Test]
        public void Construction_ShouldThrowArgumentException_WhenWinnerAndLoserScoreAreEqual()
        {
            Action action = () => new MatchResult(2, 2);
            action.Should().Throw<ArgumentException>().Which.Message.Should().BeEquivalentTo("WinnerScore must be greater than LoserScore");
        }

        [Test]
        public void Construction_ShouldCreateInstance_WhenWinnerScoreIsGreaterThan()
        {
            var matchResult = new MatchResult(11, 2);
            matchResult.Should().NotBeNull();
            matchResult.WinnerScore.Should().Be(11);
            matchResult.LoserScore.Should().Be(2);
            matchResult.ToString().Should().BeEquivalentTo("11-2");
        }

        [Test]
        public void Construction_ShouldBeEquivalent_WhenBothMatchResultsHaveTheSameScore()
        {
            var left = new MatchResult(11,5);
            var right = new MatchResult(11, 5);
            (left == right).Should().BeTrue();
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEquivalent_WhenMatchResultsHaveDifferentScore()
        {
            var left = new MatchResult(11, 6);
            var right = new MatchResult(11, 5);
            (left != right).Should().BeTrue();
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            MatchResult left = null;
            var right = new MatchResult(2,1);
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new MatchResult(2,1);
            MatchResult right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldBeEqual_WhenBothSidesAreNull()
        {
            MatchResult left = null;
            MatchResult right = null;
            (left == right).Should().BeTrue();
        }

        [Test]
        public void GetScoreDifferential_ShouldReturn5_WhenScoreThen11to6()
        {
            var matchResult = new MatchResult(11,6);
            matchResult.GetScoreDifferential().Should().Be(5);
        }

        [Test]
        public void GetScoreDifferential_ShouldReturn1_WhenScoreThen11to10()
        {
            var matchResult = new MatchResult(11, 10);
            matchResult.GetScoreDifferential().Should().Be(1);
        }
    }
}