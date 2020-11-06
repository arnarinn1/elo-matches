using System;

namespace EloMatches.Shared.Exceptions
{
    public class CommandValidationException : Exception
    {
        public string CommandType { get; set; }

        public CommandValidationException(string commandType, Exception innerException) : base($"Command Validation Errors for type = '{commandType}'", innerException)
        {
            CommandType = commandType;
        }
    }
}