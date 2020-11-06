using System;
using EloMatches.Api.Features.Players.Commands.ChangePlayerNames;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.Api.Features.Players.Commands.ChangePlayerNames
{
    [TestFixture, Category("Unit")]
    public class ChangePlayerNameCommandValidatorTests
    {
        private static readonly PlayerId PlayerId = new PlayerId(Guid.NewGuid());
        private static readonly Name UserName = new Name("UserName");
        private static readonly Name DisplayName = new Name("DisplayName");

        private ChangePlayerNamesCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new ChangePlayerNamesCommandValidator();
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenPlayerIdAndNameIsNull()
        {
            var result = _validator.Validate(new ChangePlayerNamesCommand(null,null, null));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(3);
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenUserNameIsNull()
        {
            var result = _validator.Validate(new ChangePlayerNamesCommand(PlayerId, null, DisplayName));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Test]
        public void Validate_ShouldBeInvalid_WhenDisplayNameIsNull()
        {
            var result = _validator.Validate(new ChangePlayerNamesCommand(PlayerId, UserName, null));
            result.IsValid.Should().BeFalse();
            result.Errors.Count.Should().Be(1);
        }

        [Test]
        public void Validate_ShouldBeValid_WhenAllFieldsAreValid()
        {
            var result = _validator.Validate(new ChangePlayerNamesCommand(PlayerId, UserName, DisplayName));
            result.IsValid.Should().BeTrue();
        }
    }
}