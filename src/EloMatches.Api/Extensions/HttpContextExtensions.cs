using System.Linq;
using Microsoft.AspNetCore.Http;

namespace EloMatches.Api.Extensions
{
    public static class HttpContextExtensions
    {
        public static bool DetermineIfRequestShouldBeLogged(this HttpContext context, params string[] pathsToIgnore)
        {
            if (pathsToIgnore == null || pathsToIgnore.Length == 0)
                return true;

            var path = context.Request.Path.ToString();

            return !pathsToIgnore.Any(x => path.Contains(x));
        }
    }
}