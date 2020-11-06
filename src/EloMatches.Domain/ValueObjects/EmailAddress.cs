using System;
using System.Collections.Generic;
using EloMatches.Domain.ValueObjects.Extensions;
using EloMatches.Shared.Exceptions;

namespace EloMatches.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public string Value { get; private set; }

        public EmailAddress(string value)
        {
            _ = value.NotNullOrWhitespace() ?? throw new ArgumentException("Value can't be null or whitespace");
            _ = value.IsValidEmail() ?? throw new InvalidEmailException(value);

            Value = value;
        }

        private EmailAddress() {}

        public override string ToString()
            => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}