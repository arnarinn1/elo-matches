using System;
using EloMatches.Api.Features.Players.Commands.CreatePlayer;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Api.Features.Players.Commands.CreatePlayer
{
    [TestFixture, Category("Unit")]
    public class CreatePlayerCommandValidatorTests
    {
        private static readonly PlayerId PlayerId = new PlayerId(Guid.NewGuid());
        private static readonly Name UserName = new Name("UserName");
        private static readonly Name DisplayName = new Name("DisplayName");
        private static readonly EmailAddress Email = new EmailAddress("email@email.com");

        private CreatePlayerCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreatePlayerCommandValidator();
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenUserNameAndDisplayNameIsNull()
        {
            var result = _validator.Validate(new CreatePlayerCommand(null, null, null, null));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(4);
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenUserNameIsNull()
        {
            var result = _validator.Validate(new CreatePlayerCommand(PlayerId, null, DisplayName, Email));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenDisplayNameIsNull()
        {
            var result = _validator.Validate(new CreatePlayerCommand(PlayerId, UserName, null, Email));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenEmailIsNull()
        {
            var result = _validator.Validate(new CreatePlayerCommand(PlayerId, UserName, DisplayName, null));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Test]
        public void Validate_ShouldBeValid_WhenBothNamesAreValid()
        {
            var result = _validator.Validate(new CreatePlayerCommand(PlayerId, UserName, DisplayName, Email));
            result.IsValid.Should().BeTrue();
        }
    }
}