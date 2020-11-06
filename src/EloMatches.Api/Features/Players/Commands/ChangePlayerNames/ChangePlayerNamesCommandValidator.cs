using FluentValidation;

namespace EloMatches.Api.Features.Players.Commands.ChangePlayerNames
{
    public class ChangePlayerNamesCommandValidator : AbstractValidator<ChangePlayerNamesCommand>
    {
        public ChangePlayerNamesCommandValidator()
        {
            RuleFor(command => command.PlayerId).NotNull();
            RuleFor(command => command.UserName).NotNull();
            RuleFor(command => command.DisplayName).NotNull();
        }
    }
}