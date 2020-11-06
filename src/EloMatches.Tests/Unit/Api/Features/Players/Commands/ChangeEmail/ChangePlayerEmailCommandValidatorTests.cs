using System;
using EloMatches.Api.Features.Players.Commands.ChangeEmail;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Api.Features.Players.Commands.ChangeEmail
{
    [TestFixture, Category("Unit")]
    public class ChangePlayerEmailCommandValidatorTests
    {
        private static readonly PlayerId PlayerId = new PlayerId(Guid.NewGuid());
        private static readonly EmailAddress Email = new EmailAddress("email@email.com");

        private ChangePlayerEmailCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new ChangePlayerEmailCommandValidator();
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenPlayerIdAndNameIsNull()
        {
            var result = _validator.Validate(new ChangePlayerEmailCommand(null, null));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(2);
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenEmailIsNull()
        {
            var result = _validator.Validate(new ChangePlayerEmailCommand(PlayerId, null));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Test]
        public void Validate_ShouldBeValid_WhenAllFieldsAreValid()
        {
            var result = _validator.Validate(new ChangePlayerEmailCommand(PlayerId, Email));
            result.IsValid.Should().BeTrue();
        }
    }
}