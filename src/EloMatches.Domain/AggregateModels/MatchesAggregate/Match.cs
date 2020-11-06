﻿using System;
using EloMatches.Domain.ValueObjects;

namespace EloMatches.Domain.AggregateModels.MatchesAggregate
{
    public class Match
    {
        public Match(MatchResult matchResult, PlayerMatchInformation winner, PlayerMatchInformation loser, MatchDate matchDate)
        {
            MatchResult = matchResult ?? throw new ArgumentNullException(nameof(matchResult));
            Winner = winner ?? throw new ArgumentNullException(nameof(winner));
            Loser = loser ?? throw new ArgumentNullException(nameof(loser));
            MatchDate = matchDate ?? throw new ArgumentNullException(nameof(matchDate));
            EntryDate = SystemTime.Now();
        }

        public int Id { get; private set; }

        public MatchResult MatchResult { get; private set; }
        public PlayerMatchInformation Winner { get; private set; }
        public PlayerMatchInformation Loser { get; private set; }
        public MatchDate MatchDate { get; private set; }
        public DateTime EntryDate { get; private set; }

        private Match() {}
    }
}