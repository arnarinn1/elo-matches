using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EloMatches.Api.Features.Matches.Commands.RegisterMatch.HttpPayloads;
using EloMatches.Domain.AggregateModels.MatchesAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Infrastructure.CommandPipeline.AbstractHandlers;

namespace EloMatches.Api.Features.Matches.Commands.RegisterMatch
{
    public class RegisterMatchCommandHandler : BaseCommandHandler<RegisterMatchCommand, RegisterMatchResponseBody>
    {
        private readonly IMatchesAfterReferenceDateRepository _repository;

        public RegisterMatchCommandHandler(IEnumerable<IDomainEventProcessor> domainEventProcessors, IMatchesAfterReferenceDateRepository repository) : base(domainEventProcessors)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task<(IReadOnlyCollection<IDomainEvent>, RegisterMatchResponseBody)> HandleCommand(RegisterMatchCommand command, CancellationToken cancellationToken = default)
        {
            var matches = await _repository.Get(command.MatchDate, command.WinnerPlayerId, command.LoserPlayerId);
            
            matches.RegisterMatch(command.MatchResult, command.MatchDate);

            await _repository.Process(matches);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return (matches.DomainEvents, new RegisterMatchResponseBody(matches.GetRegisteredMatchId()));
        }
    }
}