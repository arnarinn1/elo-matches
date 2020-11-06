using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate
{
    public interface IMatchesAfterReferenceDateRepository : IAggregateRepository<MatchesAfterReferenceDate>
    {
        Task<MatchesAfterReferenceDate> Get(MatchDate matchDate, PlayerId winnerPlayerId, PlayerId loserPlayerId);
        Task Process(MatchesAfterReferenceDate aggregate);
    }
}