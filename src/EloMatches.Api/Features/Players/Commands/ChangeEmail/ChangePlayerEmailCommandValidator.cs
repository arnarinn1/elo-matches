using FluentValidation;

namespace EloMatches.Api.Features.Players.Commands.ChangeEmail
{
    public class ChangePlayerEmailCommandValidator : AbstractValidator<ChangePlayerEmailCommand>
    {
        public ChangePlayerEmailCommandValidator()
        {
            RuleFor(command => command.PlayerId).NotNull();
            RuleFor(command => command.Email).NotNull();
        }
    }
}