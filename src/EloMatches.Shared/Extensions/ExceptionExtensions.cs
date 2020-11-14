using System;

namespace EloMatches.Shared.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetInnermostExceptionMessage(this Exception exception)
        {
            while (true)
            {
                if (exception.InnerException == null)
                    return exception.Message;

                exception = exception.InnerException;
            }
        }
    }
}