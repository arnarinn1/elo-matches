using System;
using Microsoft.AspNetCore.Builder;

namespace EloMatches.Api.Application.Middleware.RequestLogging
{
    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            var options = new RequestLoggingOptions();
            return builder.UseMiddleware<RequestLoggingMiddleware>(options);
        }

        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder, Action<RequestLoggingOptions> requestLoggingOptions)
        {
            var options = new RequestLoggingOptions();
            requestLoggingOptions(options);
            return builder.UseMiddleware<RequestLoggingMiddleware>(options);
        }
    }
}