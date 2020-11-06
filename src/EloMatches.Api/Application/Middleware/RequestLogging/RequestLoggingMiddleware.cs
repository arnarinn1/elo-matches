using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EloMatches.Api.Application.Middleware.RequestLogging
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestLoggingOptions _options;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, RequestLoggingOptions options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Invoke(HttpContext context)
        {
            var createLogEntry = _options.ShouldLogRequest.Invoke(context);

            if (!createLogEntry)
            {
                await _next(context);
                return;
            }

            var timer = Stopwatch.StartNew();
            await _next(context);
            timer.Stop();

            _logger.LogInformation("HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms.",
                context.Request.Method,
                context.Request.Path.ToString(),
                context.Response.StatusCode,
                timer.ElapsedMilliseconds);
        }
    }
}