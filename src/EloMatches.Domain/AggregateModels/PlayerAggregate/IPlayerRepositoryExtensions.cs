using System.Threading.Tasks;
using EloMatches.Shared.Exceptions;

namespace EloMatches.Domain.AggregateModels.PlayerAggregate
{
    // ReSharper disable once InconsistentNaming
    public static class IPlayerRepositoryExtensions
    {
        public static async ValueTask<Player> GetOrThrowIfNotFound(this IPlayerRepository repository, PlayerId playerId)
        {
            var player = await repository.Get(playerId);

            if (player == null)
                throw new DomainAggregateNotFoundException(playerId, "Player");

            return player;
        }
    }
}