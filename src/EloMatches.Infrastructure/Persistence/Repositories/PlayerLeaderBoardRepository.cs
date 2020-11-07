using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            var players = await _context.Set<PlayerOnLeaderBoard>().ToListAsync();
            return PlayerLeaderBoardReflectionFactory.Create(players);
        }

        public async Task Process(PlayerLeaderBoard leaderBoard)
        {
            var addedEvents = leaderBoard.DomainEvents.OfType<PlayerAddedToLeaderBoard>().ToArray();
            if (addedEvents.Length == 0)
                return;

            var players = PlayerLeaderBoardReflectionFactory.GetPlayers(leaderBoard);
            var addedPlayers = addedEvents.Select(playerRankingDomainEvent => players.Single(x => x.PlayerId == playerRankingDomainEvent.PlayerId)).ToList();
            
            await _context.Set<PlayerOnLeaderBoard>().AddRangeAsync(addedPlayers);
        }

        private static class PlayerLeaderBoardReflectionFactory
        {
            private static Func<ICollection<PlayerOnLeaderBoard>, PlayerLeaderBoard> CreateInstanceFactory { get; }
            private static Func<PlayerLeaderBoard, ICollection<PlayerOnLeaderBoard>> GetPlayersFactory { get; }

            static PlayerLeaderBoardReflectionFactory()
            {
                var constructor = typeof(PlayerLeaderBoard).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();

                CreateInstanceFactory = players =>
                {
                    return (PlayerLeaderBoard)constructor.Invoke(new object[] { players });
                };

                var playersProperty = typeof(PlayerLeaderBoard).GetProperty("Players", BindingFlags.Instance | BindingFlags.NonPublic);

                if (playersProperty == null)
                    throw new ArgumentNullException(nameof(playersProperty));

                GetPlayersFactory = leaderBoard => (ICollection<PlayerOnLeaderBoard>)playersProperty.GetValue(leaderBoard);
            }

            internal static PlayerLeaderBoard Create(ICollection<PlayerOnLeaderBoard> players)
            {
                return CreateInstanceFactory(players);
            }

            internal static ICollection<PlayerOnLeaderBoard> GetPlayers(PlayerLeaderBoard leaderBoard)
                => GetPlayersFactory(leaderBoard);
        }
    }
}