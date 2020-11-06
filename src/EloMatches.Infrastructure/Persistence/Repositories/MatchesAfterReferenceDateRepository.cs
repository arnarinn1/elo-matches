using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EloMatches.Domain.AggregateModels.MatchesAggregate;
using EloMatches.Domain.AggregateModels.MatchesAggregate.DomainEvents;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace EloMatches.Infrastructure.Persistence.Repositories
{
    public class MatchesAfterReferenceDateRepository : IMatchesAfterReferenceDateRepository
    {
        private readonly EloMatchesCommandContext _context;

        public MatchesAfterReferenceDateRepository(EloMatchesCommandContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<MatchesAfterReferenceDate> Get(MatchDate matchDate, PlayerId winnerPlayerId, PlayerId loserPlayerId)
        {
            var date = matchDate.Value;

            var matchSet = _context.Set<Match>();

            var matchesThatOccurredAfterMatchDate = await matchSet.Where(x => x.MatchDate.Value > date).ToListAsync();

            var winnerStats = await GetPlayerStatsBeforeGame(winnerPlayerId);
            var loserStats = await GetPlayerStatsBeforeGame(loserPlayerId);

            return MatchesReflectionFactory.Create(matchDate, winnerStats, loserStats, matchesThatOccurredAfterMatchDate);

            async Task<PlayerStatsBeforeMatch> GetPlayerStatsBeforeGame(PlayerId playerId)
            {
                var id = playerId.Id;

                var lastMatch = await matchSet
                    .Where(x => x.MatchDate.Value < date && (x.Winner.PlayerId == id || x.Loser.PlayerId == id))
                    .OrderByDescending(x => x.MatchDate.Value)
                    .Select(x => new
                    {
                        WinnerId = x.Winner.PlayerId,
                        LoserId = x.Loser.PlayerId,
                        WinnerEloRating = x.Winner.TotalEloRatingAfterGame,
                        LoserEloRating = x.Loser.TotalEloRatingAfterGame,
                        GameNumberForWinner = x.Winner.GameNumber,
                        GameNumberForLoser = x.Loser.GameNumber
                    })
                    .FirstOrDefaultAsync();

                if (lastMatch == null)
                    return new PlayerStatsBeforeMatch(playerId, 1000, 0);

                return id == lastMatch.WinnerId
                    ? new PlayerStatsBeforeMatch(playerId, lastMatch.WinnerEloRating, lastMatch.GameNumberForWinner)
                    : new PlayerStatsBeforeMatch(playerId, lastMatch.LoserEloRating, lastMatch.GameNumberForLoser);
            };
        }

        public async Task Process(MatchesAfterReferenceDate aggregate)
        {
            if (!aggregate.DomainEvents.Any(x => x is MatchRegistered)) 
                return;

            await _context.Set<Match>().AddAsync(MatchesReflectionFactory.GetRegisteredMatch(aggregate));
        }

        private static class MatchesReflectionFactory 
        {
            private static Func<MatchDate, PlayerStatsBeforeMatch, PlayerStatsBeforeMatch, ICollection<Match>, MatchesAfterReferenceDate> CreateInstanceFactory { get; }
            private static Func<MatchesAfterReferenceDate, Match> GetRegisteredMatchFactory { get; }

            static MatchesReflectionFactory()
            {
                var constructor = typeof(MatchesAfterReferenceDate).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Single();

                CreateInstanceFactory = (matchDate, winnerStats, loserStats, allMatches) =>
                {
                    return (MatchesAfterReferenceDate) constructor.Invoke(new object[]{matchDate, winnerStats, loserStats, allMatches});
                };
                
                var propertyForRegisteredMatches = typeof(MatchesAfterReferenceDate).GetProperty("RegisteredMatch", BindingFlags.Instance | BindingFlags.NonPublic);

                if (propertyForRegisteredMatches == null)
                    throw new ArgumentNullException(nameof(propertyForRegisteredMatches));

                GetRegisteredMatchFactory = matches => (Match)propertyForRegisteredMatches.GetValue(matches);
            }

            internal static MatchesAfterReferenceDate Create(MatchDate referenceDate, PlayerStatsBeforeMatch winnerStats, PlayerStatsBeforeMatch loserStats, ICollection<Match> allMatchesRegisteredAfterMatchDate)
            {   
                return CreateInstanceFactory(referenceDate, winnerStats, loserStats, allMatchesRegisteredAfterMatchDate);
            }

            internal static Match GetRegisteredMatch(MatchesAfterReferenceDate aggregate) => GetRegisteredMatchFactory(aggregate);
        }
    }
}