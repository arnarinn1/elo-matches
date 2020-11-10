using System;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.Domain.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class NameTests
    {
        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenValueIsNull()
        {
            Action action = () => new Name(null);
            action.Should().Throw<ArgumentException>().And.Message.Should().Be("Name can't be null or whitespace");
        }

        [Test]
        public void Constructor_ShouldThrowArgumentException_WhenValueIsWhitespace()
        {
            Action action = () => new Name(" ");
            action.Should().Throw<ArgumentException>().And.Message.Should().Be("Name can't be null or whitespace");
        }

        [Test]
        public void Constructor_ShouldCreateInstance_WhenValueIsValidString()
        {
            var name = new Name("Name");
            name.Value.Should().BeEquivalentTo("Name");
        }

        [Test]
        public void Constructor_ShouldBeEqual_WhenTwoNamesHaveTheSameValue()
        {
            var left = new Name("Name");
            var right = new Name("Name");
            left.Should().Be(right);
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenTwoNamesHaveTheDifferentValues()
        {
            var left = new Name("Name");
            var right = new Name("name");
            left.Should().NotBe(right);
        }

        [Test]
        public void Constructor_ShouldOnlyTakeFirst128CharactersInName_WhenValueIsLongerThan128Characters()
        {
            var name = new Name("0".PadRight(130, '0'));
            var expectedName = new Name("0".PadRight(128, '0'));
            name.Should().BeEquivalentTo(expectedName);
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            Name left = null;
            var right = new Name("Name");
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new Name("Name");
            Name right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldBeEqual_WhenBothSidesAreNull()
        {
            Name left = null;
            Name right = null;
            (left == right).Should().BeTrue();
        }
    }
}