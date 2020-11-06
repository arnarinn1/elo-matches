using System;

namespace EloMatches.Shared.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public string Email { get; set; }

        public InvalidEmailException(string email) : base($"Email = '{email} is not a valid email.'")
        {
            Email = email;
        }
    }
}