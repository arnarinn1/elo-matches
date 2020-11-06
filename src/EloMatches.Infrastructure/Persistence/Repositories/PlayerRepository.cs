using System;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;

namespace EloMatches.Infrastructure.Persistence.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly EloMatchesCommandContext _context;

        public PlayerRepository(EloMatchesCommandContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Add(Player player)
        {
            await _context.Set<Player>().AddAsync(player);
        }

        public ValueTask<Player> Get(PlayerId playerId)
        {
            return _context.Set<Player>().FindAsync(playerId.Id);
        }

        public IUnitOfWork UnitOfWork => _context;
    }
}