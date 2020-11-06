using System;
using Microsoft.AspNetCore.Http;

namespace EloMatches.Api.Application.Middleware.Exceptions
{
    public class ApiExceptionOptions
    {
        public Action<HttpContext, Exception, ApiError> AddResponseDetails { get; set; }
    }
}