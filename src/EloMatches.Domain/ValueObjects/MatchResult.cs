using System;
using System.Collections.Generic;

namespace EloMatches.Domain.ValueObjects
{
    public class MatchResult : ValueObject
    {
        public int WinnerScore { get; private set; }
        public int LoserScore { get; private set; }

        public MatchResult(int winnerScore, int loserScore)
        {
            if (loserScore < 0)
                throw new ArgumentException("LoserScore can't be less than zero", nameof(loserScore));

            if (winnerScore <= loserScore)
                throw new ArgumentException("WinnerScore must be greater than LoserScore");

            WinnerScore = winnerScore;
            LoserScore = loserScore;
        }

        private MatchResult() { }

        public int GetScoreDifferential()
        {
            return WinnerScore - LoserScore;
        }
        
        public override string ToString()
        {
            return $"{WinnerScore}-{LoserScore}";
        }
     
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return WinnerScore;
            yield return LoserScore;
        }
    }
}