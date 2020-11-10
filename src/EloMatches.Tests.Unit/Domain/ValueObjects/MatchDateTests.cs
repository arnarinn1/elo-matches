using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.Domain.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class MatchDateTests
    {
        [TearDown]
        public void RunAfterEachTest()
        {
            SystemTime.Reset();
        }

        [Test]
        public void Construction_ShouldCreateInstance_WhenNullIsPassedAsParameterToConstructor()
        {
            SystemTime.Set(1.January(2020));
            var matchDate = new MatchDate();
            matchDate.Should().NotBeNull();
            matchDate.Value.Should().Be(1.January(2020));
        }

        [Test]
        public void Construction_ShouldCreateInstance_WhenDateIsPassedAsParameterToConstructor()
        {
            var matchDate = new MatchDate(2.January(2020));
            matchDate.Should().NotBeNull();
            matchDate.Value.Should().Be(2.January(2020));
        }

        [Test]
        public void Construction_ShouldBeEquivalent_WhenBothMatchResultsHaveTheSameScore()
        {
            SystemTime.Set(1.January(2020));
            var left = new MatchDate();
            var right = new MatchDate();
            (left == right).Should().BeTrue();
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEquivalent_WhenMatchResultsHaveDifferentScore()
        {
            SystemTime.Set(1.January(2020));
            var left = new MatchDate();

            SystemTime.Set(1.January(2021));
            var right = new MatchDate();
            
            (left != right).Should().BeTrue();
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            MatchDate left = null;
            var right = new MatchDate();
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new MatchDate();
            MatchDate right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Construction_ShouldBeEqual_WhenBothSidesAreNull()
        {
            MatchDate left = null;
            MatchDate right = null;
            (left == right).Should().BeTrue();
        }

        [Test]
        public void IsEarlier_ShouldBeEarlier_WhenDateIsAnEarlierDate()
        {
            var date = new MatchDate(1.November(2020));

            date.IsEarlierThan(new MatchDate(1.November(2020))).Should().BeFalse();
            date.IsEarlierThan(new MatchDate(31.October(2020))).Should().BeFalse();
            date.IsEarlierThan(new MatchDate(30.April(2020))).Should().BeFalse();
        }

        [Test]
        public void IsEarlier_ShouldNotBeEarlier_WhenDateIsANewerDate()
        {
            var date = new MatchDate(1.November(2020));

            date.IsEarlierThan(new MatchDate(1.November(2020).AddMinutes(1))).Should().BeTrue();
            date.IsEarlierThan(new MatchDate(1.November(2020).At(10, 20))).Should().BeTrue();
            date.IsEarlierThan(new MatchDate(1.November(2021))).Should().BeTrue();
        }
    }
}