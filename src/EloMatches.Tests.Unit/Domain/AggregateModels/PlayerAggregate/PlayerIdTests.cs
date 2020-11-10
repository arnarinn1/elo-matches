using System;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.Domain.AggregateModels.PlayerAggregate
{
    [TestFixture, Category("Unit")]
    public class PlayerIdTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenValueIsEmptyGuid()
        {
            Action action = () => new PlayerId(Guid.Empty);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Constructor_ShouldCreateInstance_WhenValueIsAGuid()
        {
            var id = Guid.NewGuid();
            var playerId = new PlayerId(id);
            playerId.Id.Should().Be(id);
        }

        [Test]
        public void Constructor_ShouldBeEqual_WhenTwoPlayerIdsHaveSameGuid()
        {
            var guid = new Guid("156B4D9E-14B6-47D1-8823-9174029D7A89");
            var left = new PlayerId(guid);
            var right = new PlayerId(guid);
            left.Should().Be(right);
            (left == right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenTwoPlayerIdsHaveDifferentGuid()
        {
            var left = new PlayerId(Guid.NewGuid());
            var right = new PlayerId(Guid.NewGuid());
            left.Should().NotBe(right);
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            PlayerId left = null;
            var right = new PlayerId(Guid.NewGuid());
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new PlayerId(Guid.NewGuid());
            PlayerId right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldBeEqual_WhenBothSidesAreNull()
        {
            PlayerId left = null;
            PlayerId right = null;
            (left == right).Should().BeTrue();
        }
    }
}