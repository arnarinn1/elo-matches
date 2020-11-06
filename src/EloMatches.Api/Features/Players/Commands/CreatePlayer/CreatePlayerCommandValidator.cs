using FluentValidation;

namespace EloMatches.Api.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        public CreatePlayerCommandValidator()
        {
            RuleFor(command => command.PlayerId).NotNull();
            RuleFor(command => command.UserName).NotNull();
            RuleFor(command => command.DisplayName).NotNull();
            RuleFor(command => command.Email).NotNull();
        }
    }
}