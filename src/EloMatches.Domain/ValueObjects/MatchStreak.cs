using System.Collections.Generic;

namespace EloMatches.Domain.ValueObjects
{
    public class MatchStreak : ValueObject
    {
        public int StreakCount { get; private set; }
        public MatchStreakType Type { get; private set; }

        public MatchStreak() : this(0, MatchStreakType.Winning) { }

        private MatchStreak(int streakCount, MatchStreakType type)
        {
            StreakCount = streakCount;
            Type = type;
        }

        public MatchStreak AddWin()
        {
            var streak = StreakCount;

            if (Type == MatchStreakType.Winning)
                streak++;
            else
                streak = 1;

            return new MatchStreak(streak, MatchStreakType.Winning);
        }

        public MatchStreak AddLoss()
        {
            var streak = StreakCount;

            if (Type == MatchStreakType.Losing)
                streak++;
            else
                streak = 1;

            return new MatchStreak(streak, MatchStreakType.Losing);
        }

        public override string ToString()
        {
            return $"{Type} - {StreakCount}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StreakCount;
            yield return (int) Type;
        }
    }
}