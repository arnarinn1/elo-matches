using System;
using System.Linq;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate;
using EloMatches.Domain.AggregateModels.PlayerLeaderBoardAggregate.DomainEvents;
using EloMatches.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace EloMatches.Infrastructure.Persistence.Repositories
{
    public class PlayerLeaderBoardRepository : IPlayerLeaderBoardRepository
    {
        private readonly EloMatchesCommandContext _context;

        public PlayerLeaderBoardRepository(EloMatchesCommandContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<PlayerLeaderBoard> Get()
        {
            var playersOnLeaderBoard = await _context.Set<PlayerOnLeaderBoard>().ToListAsync();
            return new PlayerLeaderBoard(playersOnLeaderBoard);
        }

        public async Task Process(PlayerLeaderBoard playerRanking)
        {
            var addedPlayers = playerRanking.DomainEvents.OfType<PlayerAddedToLeaderBoard>().Select(playerRankingDomainEvent => playerRanking.Players.Single(x => x.PlayerId == playerRankingDomainEvent.PlayerId)).ToList();
            await _context.Set<PlayerOnLeaderBoard>().AddRangeAsync(addedPlayers);
        }
    }
}