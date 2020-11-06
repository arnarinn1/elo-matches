using System;
using System.Collections.Generic;
using System.Linq;
using EloMatches.Domain.ValueObjects;
using EloMatches.Shared.Exceptions;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace EloMatches.Tests.Unit.ValueObjects
{
    [TestFixture, Category("Unit")]
    public class EmailAddressTests
    {
        [Test]
        public void Construction_ShouldThrowArgumentException_WhenValueIsNullOrWhitespace()
        {
            Action nullAction = () => new EmailAddress(null);
            Action whitespaceAction = () => new EmailAddress("");

            nullAction.Should().Throw<ArgumentException>();
            whitespaceAction.Should().Throw<ArgumentException>();
        }

        [Test]
        public void Construction_ShouldThrowInvalidEmailException_WhenValueIsNotAnValidEmail()
        {
            var actions = new List<Action>
            {
                () => new EmailAddress("email"),
                () => new EmailAddress("email.com"),
                () => new EmailAddress("email@@email.com"),
                () => new EmailAddress("@email"),
                () => new EmailAddress("1"),
                () => new EmailAddress("email@example..com")
            };

            foreach (var result in actions.Select(x => x.Should()))
            {
                result.Throw<InvalidEmailException>();
            }
        }

        [Test]
        public void Construction_ShouldCreateInstance_WhenEmailIsValid()
        {
            var email = new EmailAddress("email@email.com");
            email.Value.Should().BeEquivalentTo("email@email.com");
        }

        [Test]
        public void Constructor_ShouldBeEqual_WhenTwoEmailsHaveTheSameValue()
        {
            var left = new EmailAddress("email@email.com");
            var right = new EmailAddress("email@email.com");
            (left == right).Should().BeTrue();
            (left != right).Should().BeFalse();
            left.Should().Be(right);
            left.GetHashCode().Should().Be(right.GetHashCode());
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenTwoEmailsHaveTheDifferentValues()
        {
            var left = new EmailAddress("email@email.com");
            var right = new EmailAddress("email1@email.com");
            (left == right).Should().BeFalse();
            (left != right).Should().BeTrue();
            left.Should().NotBe(right);
            left.GetHashCode().Should().NotBe(right.GetHashCode());
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenLeftSideIsNull()
        {
            EmailAddress left = null;
            var right = new EmailAddress("email@email.com");
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldNotBeEqual_WhenRightSideIsNull()
        {
            var left = new EmailAddress("email@email.com");
            EmailAddress right = null;
            (left != right).Should().BeTrue();
        }

        [Test]
        public void Constructor_ShouldBeEqual_WhenBothSidesAreNull()
        {
            EmailAddress left = null;
            EmailAddress right = null;
            (left == right).Should().BeTrue();
        }
    }
}