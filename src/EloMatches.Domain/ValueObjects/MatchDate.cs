using System;
using System.Collections.Generic;

namespace EloMatches.Domain.ValueObjects
{
    public class MatchDate : ValueObject
    {
        public DateTime Value { get; private set; }

        public MatchDate(DateTime? date = null)
        {
            Value = date ?? SystemTime.Now();
        }

        private MatchDate() { }

        public static implicit operator DateTime(MatchDate matchDate) => matchDate.Value;

        public bool IsEarlierThan(MatchDate matchDate)
        {
            _ = matchDate ?? throw new ArgumentNullException(nameof(matchDate));

            return Value < matchDate;
        }

        public override string ToString()
        {
            return $"{Value:d}";
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}