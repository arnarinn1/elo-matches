using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class ScoreStatisticsTests
    {
        [Test]
        public void Construction_ShouldBeEquivalent_WhenBothScoreStatisticsAreTheSame()
        {
            var left = new ScoreStatistics();
            var right = new ScoreStatistics();
            (left == right).Should().BeTrue();
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEquivalent_WhenScoreStatisticsAreNotTheSame()
        {
            var left = new ScoreStatistics();
            var right = new ScoreStatistics().AddWinningScores(new MatchResult(1,0));

            (left != right).Should().BeTrue();
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            ScoreStatistics left = null;
            var right = new ScoreStatistics();
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new ScoreStatistics();
            ScoreStatistics right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldBeEqual_WhenBothSidesAreNull()
        {
            ScoreStatistics left = null;
            ScoreStatistics right = null;
            (left == right).Should().BeTrue();
        }

        [Test]
        public void AddWinningScores_ShouldCalculateScoresCorrectly_WhenOneWinningMatchIsAdded()
        {
            var scoreStatistics = new ScoreStatistics();
            scoreStatistics = scoreStatistics.AddWinningScores(new MatchResult(11, 0));

            scoreStatistics.ScorePlus.Should().Be(11);
            scoreStatistics.ScoreMinus.Should().Be(0);
            scoreStatistics.ScoreDifferential.Should().Be(11);
        }

        [Test]
        public void AddWinningScoresAndAddLosingScores_ShouldCalculateScoresCorrectly_WhenOneWinningMatchAndOneLosingMatchIsAdded()
        {
            var scoreStatistics = new ScoreStatistics();
            scoreStatistics = scoreStatistics.AddWinningScores(new MatchResult(11, 0));
            scoreStatistics = scoreStatistics.AddLosingScores(new MatchResult(11, 2));

            scoreStatistics.ScorePlus.Should().Be(13);
            scoreStatistics.ScoreMinus.Should().Be(11);
            scoreStatistics.ScoreDifferential.Should().Be(2);
        }

        [Test]
        public void AddLosingScores_ShouldCalculateScoresCorrectly_WhenOneLosingMatchIsAdded()
        {
            var scoreStatistics = new ScoreStatistics();
            scoreStatistics = scoreStatistics.AddLosingScores(new MatchResult(11, 2));

            scoreStatistics.ScorePlus.Should().Be(2);
            scoreStatistics.ScoreMinus.Should().Be(11);
            scoreStatistics.ScoreDifferential.Should().Be(-9);
        }
    }
}