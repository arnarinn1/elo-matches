using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerRankingAggregate
{
    public interface IPlayerRankingRepository : IAggregateRepository<PlayerRanking>
    {
        Task Add(PlayerRanking playerRanking);
        ValueTask<PlayerRanking> Get(PlayerId playerId);
    }
}