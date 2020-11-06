using System;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.AggregateModels.PlayerRankingAggregate.DomainEvents;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerRankingAggregate
{
    public class PlayerRanking : Aggregate, IAggregateRoot
    {
        public PlayerRanking(PlayerId playerId)
        {
            PlayerId = playerId;

            EloRatingStatistics = new EloRatingStatistics();
            NumberOfMatchesStatistics = new NumberOfMatchesStatistics();
            ScoreStatistics = new ScoreStatistics();
            Streak = new MatchStreak();

            LastMatchDate = null;

            AddDomainEvent(new PlayerRankingCreated(playerId, EloRatingStatistics, NumberOfMatchesStatistics, ScoreStatistics, Streak, LastMatchDate));
        }

        private PlayerRanking() { }

        public Guid PlayerId { get; private set; }

        public EloRatingStatistics EloRatingStatistics { get; private set; }
        public NumberOfMatchesStatistics NumberOfMatchesStatistics { get; private set; }
        public ScoreStatistics ScoreStatistics { get; private set; }
        public MatchStreak Streak { get; private set; }
        
        public DateTime? LastMatchDate { get; private set; }

        public void AddWin(MatchResult matchResult, MatchDate matchDate, EloCalculationDifference calculationDifference)
        {
            NumberOfMatchesStatistics = NumberOfMatchesStatistics.AddWin();
            ScoreStatistics = ScoreStatistics.AddWinningScores(matchResult);
            Streak = Streak.AddWin();
            EloRatingStatistics = EloRatingStatistics.AddWin(calculationDifference, NumberOfMatchesStatistics.PlayedGames);
            LastMatchDate = matchDate;

            AddDomainEvent(new WinAddedToPlayerRanking(new PlayerId(PlayerId), EloRatingStatistics, NumberOfMatchesStatistics, ScoreStatistics, Streak, LastMatchDate));
        }

        public void AddLoss(MatchResult matchResult, MatchDate matchDate, EloCalculationDifference calculationDifference)
        {
            NumberOfMatchesStatistics = NumberOfMatchesStatistics.AddLoss();
            ScoreStatistics = ScoreStatistics.AddLosingScores(matchResult);
            Streak = Streak.AddLoss();
            EloRatingStatistics = EloRatingStatistics.AddLoss(calculationDifference, NumberOfMatchesStatistics.PlayedGames);
            LastMatchDate = matchDate;

            AddDomainEvent(new LossAddedToPlayerRanking(new PlayerId(PlayerId), EloRatingStatistics, NumberOfMatchesStatistics, ScoreStatistics, Streak, LastMatchDate));
        }
    }
}