using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using EloMatches.Api;
using EloMatches.Api.Features.Matches.Commands.RegisterMatch;
using EloMatches.Api.Features.Players.Commands.Activations;
using EloMatches.Api.Features.Players.Commands.ChangeEmail;
using EloMatches.Api.Features.Players.Commands.ChangePlayerNames;
using EloMatches.Api.Features.Players.Commands.CreatePlayer;
using EloMatches.Shared.Extensions;
using FluentValidation;
using NUnit.Framework;

namespace EloMatches.Tests.Unit.ExportedTypesTests
{
    [TestFixture, Category("Unit")]
    public class RegisteredCommandValidatorsCollectionAssertationTests
    {
        private static readonly IDictionary<Type, ISet<Type>> Validators =
            new Dictionary<Type, ISet<Type>>
            {
                //Players
                {typeof(CreatePlayerCommand), new HashSet<Type>{typeof(CreatePlayerCommandValidator)}},
                {typeof(ChangePlayerNamesCommand), new HashSet<Type>{typeof(ChangePlayerNamesCommandValidator)}},
                {typeof(ChangePlayerEmailCommand), new HashSet<Type>{typeof(ChangePlayerEmailCommandValidator)}},
                {typeof(SetPlayerActiveStatusCommand), new HashSet<Type>()},

                //MatchesAfterReferenceDate
                {typeof(RegisterMatchCommand), new HashSet<Type>()}
            };

        [Test]
        public void CommandValidatorAssertation()
        {
            var commands = ReflectionHelpers.GetAllTypesImplementingInterface(typeof(ICommand), typeof(Startup).Assembly);

            var validators = ReflectionHelpers.GetAllTypesImplementingOpenGenericType(typeof(IValidator<>), typeof(Startup).Assembly).ToArray();

            foreach (var command in commands)
            {
                if (!Validators.TryGetValue(command, out var validatorMap))
                    Assert.Fail($"Missing validator mapping for command = '{command.Name}'");

                var registeredValidators = validators.Where(x => x.GetInterfaces().Any(y => typeof(IValidator<>).MakeGenericType(command).IsAssignableFrom(y))).ToArray();

                foreach (var validator in registeredValidators)
                {
                    if (!validatorMap.Contains(validator))
                        Assert.Fail($"Found validator = '{validator.Name}' that is incorrectly registered in test setup for command = '{command.Name}'");
                }
            }
        }
    }
}