using System;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerRankingAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Infrastructure.Persistence.Repositories
{
    public class PlayerRankingRepository : IPlayerRankingRepository
    {
        private readonly EloMatchesCommandContext _context;

        public PlayerRankingRepository(EloMatchesCommandContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;
        
        public async Task Add(PlayerRanking playerRanking)
        {
            await _context.Set<PlayerRanking>().AddAsync(playerRanking);
        }

        public ValueTask<PlayerRanking> Get(PlayerId playerId)
        {
            return _context.Set<PlayerRanking>().FindAsync(playerId.Id);
        }
    }
}