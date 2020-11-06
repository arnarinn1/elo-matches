using System;
using System.Collections.Generic;

namespace EloMatches.Domain.ValueObjects
{
    public class ScoreStatistics : ValueObject
    {
        public int ScorePlus { get; private set; }
        public int ScoreMinus { get; private set; }
        public int ScoreDifferential { get; private set; }

        public ScoreStatistics() : this(0, 0, 0) { }

        private ScoreStatistics(int scorePlus, int scoreMinus, int scoreDifferential)
        {
            ScorePlus = scorePlus;
            ScoreMinus = scoreMinus;
            ScoreDifferential = scoreDifferential;
        }

        public ScoreStatistics AddWinningScores(MatchResult matchResult)
        {
            _ = matchResult ?? throw new ArgumentNullException(nameof(matchResult));

            var scorePlus = ScorePlus + matchResult.WinnerScore;
            var scoreMinus = ScoreMinus + matchResult.LoserScore;
            var scoreDifferential = ScoreDifferential + matchResult.GetScoreDifferential();

            return new ScoreStatistics(scorePlus, scoreMinus, scoreDifferential);
        }

        public ScoreStatistics AddLosingScores(MatchResult matchResult)
        {
            _ = matchResult ?? throw new ArgumentNullException(nameof(matchResult));

            var scorePlus = ScorePlus + matchResult.LoserScore;
            var scoreMinus = ScoreMinus + matchResult.WinnerScore;
            var scoreDifferential = ScoreDifferential - matchResult.GetScoreDifferential();

            return new ScoreStatistics(scorePlus, scoreMinus, scoreDifferential);
        }

        public override string ToString()
        {
            return $"ScorePlus = '{ScorePlus}' - ScoreMinus = '{ScoreMinus}' - ScoreDifferential = '{ScoreDifferential}'";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ScorePlus;
            yield return ScoreMinus;
        }
    }
}