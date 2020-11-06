using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Shared.Exceptions;

namespace EloMatches.Domain.AggregateModels.PlayerRankingAggregate
{
    // ReSharper disable once InconsistentNaming
    public static class IPlayerRankingRepositoryExtensions
    {
        public static async ValueTask<PlayerRanking> GetOrThrowIfNotFound(this IPlayerRankingRepository repository, PlayerId playerId)
        {
            var playerRanking = await repository.Get(playerId);

            if (playerRanking == null)
                throw new DomainAggregateNotFoundException(playerId, "PlayerRanking");

            return playerRanking;
        }
    }
}