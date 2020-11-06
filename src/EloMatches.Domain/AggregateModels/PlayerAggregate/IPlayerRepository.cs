using System.Threading.Tasks;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate
{
    public interface IPlayerRepository : IAggregateRepository<Player>
    {
        Task Add(Player player);
        ValueTask<Player> Get(PlayerId playerId);
    }
}