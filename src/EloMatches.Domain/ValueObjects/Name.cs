using System;
using System.Collections.Generic;
using EloMatches.Domain.ValueObjects.Extensions;

namespace EloMatches.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; private set; }

        private const int MaxLengthOfName = 128;

        public Name(string value)
        {
            _ = value.NotNullOrWhitespace() ?? throw new ArgumentException("Name can't be null or whitespace");

            Value = value.Length > MaxLengthOfName
                ? value.Substring(0, MaxLengthOfName)
                : value;
        }

        private Name() {}

        public override string ToString() => Value;
        public static implicit operator string(Name name) => name.Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}