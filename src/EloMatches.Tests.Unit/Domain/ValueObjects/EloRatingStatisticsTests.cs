using System;
using System.Reflection;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.Domain.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class EloRatingStatisticsTests
    {
        [Test]
        public void Construction_ShouldBeEquivalent_WhenBothStatisticsAreTheSame()
        {
            var left = new EloRatingStatistics();
            var right = new EloRatingStatistics();
            (left == right).Should().BeTrue();
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEquivalent_WhenStatisticsAreNotTheSame()
        {
            var left = new EloRatingStatistics();
            var right = new EloRatingStatistics().AddWin(CreateCalculation(6), 1);

            (left != right).Should().BeTrue();
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            EloRatingStatistics left = null;
            var right = new EloRatingStatistics();
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new EloRatingStatistics();
            EloRatingStatistics right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldBeEqual_WhenBothSidesAreNull()
        {
            EloRatingStatistics left = null;
            EloRatingStatistics right = null;
            (left == right).Should().BeTrue();
        }

        [Test]
        public void AddWin_ShouldHaveStatisticsRemainTheSame_WhenEloRatingGainedIsZero()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddWin(CreateCalculation(0), 1);

            eloStatistics.CurrentEloRating.Should().Be(1000);
            eloStatistics.AverageEloRating.Should().Be(1000);
            eloStatistics.HighestEloRating.Should().Be(1000);
            eloStatistics.LowestEloRating.Should().Be(1000);
            eloStatistics.EloRatingDifference.Should().Be(0);
        }

        [Test]
        public void AddWin_ShouldUpdateStatisticsForOneWin_WhenOneWinIsAdded()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddWin(CreateCalculation(5), 1);

            eloStatistics.CurrentEloRating.Should().Be(1005);
            eloStatistics.AverageEloRating.Should().Be(1005);
            eloStatistics.HighestEloRating.Should().Be(1005);
            eloStatistics.LowestEloRating.Should().Be(1000);
            eloStatistics.EloRatingDifference.Should().Be(5);
        }

        [Test]
        public void AddWin_ShouldUpdateStatisticsForTwoWins_WhenTwoWinsAreAdded()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddWin(CreateCalculation(5), 1);
            eloStatistics = eloStatistics.AddWin(CreateCalculation(15), 2);

            eloStatistics.CurrentEloRating.Should().Be(1020);
            eloStatistics.AverageEloRating.Should().Be(1012.5m);
            eloStatistics.HighestEloRating.Should().Be(1020);
            eloStatistics.LowestEloRating.Should().Be(1000);
            eloStatistics.EloRatingDifference.Should().Be(20);
        }

        [Test]
        public void AddLoss_ShouldHaveStatisticsRemainTheSame_WhenEloRatingGainedIsZero()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddLoss(CreateCalculation(0), 1);

            eloStatistics.CurrentEloRating.Should().Be(1000);
            eloStatistics.AverageEloRating.Should().Be(1000);
            eloStatistics.HighestEloRating.Should().Be(1000);
            eloStatistics.LowestEloRating.Should().Be(1000);
            eloStatistics.EloRatingDifference.Should().Be(0);
        }

        [Test]
        public void AddLoss_ShouldUpdateStatisticsForOneLoss_WhenOneLossIsAdded()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddLoss(CreateCalculation(5), 1);

            eloStatistics.CurrentEloRating.Should().Be(995);
            eloStatistics.AverageEloRating.Should().Be(995);
            eloStatistics.HighestEloRating.Should().Be(1000);
            eloStatistics.LowestEloRating.Should().Be(995);
            eloStatistics.EloRatingDifference.Should().Be(-5);
        }

        [Test]
        public void AddLoss_ShouldUpdateStatisticsForTwoLosses_WhenTwoLossesAreAdded()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddLoss(CreateCalculation(5), 1);
            eloStatistics = eloStatistics.AddLoss(CreateCalculation(15), 2);

            eloStatistics.CurrentEloRating.Should().Be(980);
            eloStatistics.AverageEloRating.Should().Be(987.5m);
            eloStatistics.HighestEloRating.Should().Be(1000);
            eloStatistics.LowestEloRating.Should().Be(980);
            eloStatistics.EloRatingDifference.Should().Be(-20);
        }

        [Test]
        public void AddWinAndLoss_ShouldUpdateStatisticsAccordingly_WhenAWinAndALossAreAdded()
        {
            var eloStatistics = new EloRatingStatistics();
            eloStatistics = eloStatistics.AddWin(CreateCalculation(10), 1);
            eloStatistics = eloStatistics.AddLoss(CreateCalculation(5), 2);

            eloStatistics.CurrentEloRating.Should().Be(1005);
            eloStatistics.AverageEloRating.Should().Be(1007.5m);
            eloStatistics.HighestEloRating.Should().Be(1010);
            eloStatistics.LowestEloRating.Should().Be(1000);
            eloStatistics.EloRatingDifference.Should().Be(5);
        }

        private static EloCalculationDifference CreateCalculation(decimal score)
        {
            var constructor = typeof(EloCalculationDifference).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { }, null);

            if (constructor == null)
                throw new ArgumentNullException(nameof(constructor), $"Private constructor not found on type = '{nameof(EloCalculationDifference)}'");

            var calculation = constructor.Invoke(new object[] { });

            var calculationType = calculation.GetType();

            calculationType.GetProperty("EloGainedForWinner")?.SetValue(calculation, score);
            calculationType.GetProperty("EloLossForLoser")?.SetValue(calculation, -score);

            return (EloCalculationDifference) calculation;
        }
    }
}