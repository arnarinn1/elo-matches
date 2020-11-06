using System;
using Microsoft.AspNetCore.Http;

namespace EloMatches.Api.Application.Middleware.RequestLogging
{
    public class RequestLoggingOptions
    {
        public Func<HttpContext, bool> ShouldLogRequest { get; set; } = context => true;
    }
}