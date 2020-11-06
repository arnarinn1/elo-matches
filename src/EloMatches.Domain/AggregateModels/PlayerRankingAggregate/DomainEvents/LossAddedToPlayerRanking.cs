using System;
using EloMatches.Domain.AggregateModels.PlayerAggregate;
using EloMatches.Domain.SeedWork;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.PlayerRankingAggregate.DomainEvents
{
    public class LossAddedToPlayerRanking : DomainEvent
    {
        public LossAddedToPlayerRanking(PlayerId playerId, EloRatingStatistics eloRatingStatistics, NumberOfMatchesStatistics numberOfMatchesStatistics, ScoreStatistics scoreStatistics, MatchStreak matchStreak, DateTime? lastMatchDate) : base(playerId, "PlayerRanking")
        {
            PlayerId = playerId;
            EloRatingStatistics = eloRatingStatistics;
            NumberOfMatchesStatistics = numberOfMatchesStatistics;
            ScoreStatistics = scoreStatistics;
            MatchStreak = matchStreak;
            LastMatchDate = lastMatchDate;
        }

        public PlayerId PlayerId { get; }
        public EloRatingStatistics EloRatingStatistics { get; }
        public NumberOfMatchesStatistics NumberOfMatchesStatistics { get; }
        public ScoreStatistics ScoreStatistics { get; }
        public MatchStreak MatchStreak { get; }
        public DateTime? LastMatchDate { get; }
    }
}