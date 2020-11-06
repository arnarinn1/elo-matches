using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class EloCalculationDifferenceTests
    {
        [Test]
        public void Construction_ShouldHave50PercentageChanceOfWinning_WhenRatingAreEqual()
        {
            var calculation = new EloCalculationDifference(1000, 1000);

            calculation.EloGainedForWinner.Should().BeGreaterThan(0);
            calculation.WinChanceForWinner.Should().Be(0.5m);
        }

        [Test]
        public void Construction_ShouldHaveChanceOfWinningAbove50_WhenRatingOfWinnerIsHigher()
        {
            var calculation = new EloCalculationDifference(1600, 1000);

            calculation.EloGainedForWinner.Should().BeGreaterThan(0);
            calculation.WinChanceForWinner.Should().BeGreaterThan(0.5m);
        }

        [Test]
        public void Construction_ShouldHaveChanceOfWinningAbove50_WhenRatingOfWinnerIsLower()
        {
            var calculation = new EloCalculationDifference(900, 1100);

            calculation.EloGainedForWinner.Should().BeGreaterThan(0);
            calculation.WinChanceForWinner.Should().BeLessThan(0.5m);
        }
    }
}