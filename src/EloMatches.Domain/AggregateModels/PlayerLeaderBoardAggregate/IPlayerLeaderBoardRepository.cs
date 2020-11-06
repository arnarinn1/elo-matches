using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate
{
    public interface IPlayerLeaderBoardRepository : IAggregateRepository<PlayerLeaderBoard>
    {
        Task<PlayerLeaderBoard> Get();
        Task Process(PlayerLeaderBoard playerRanking);
    }
}