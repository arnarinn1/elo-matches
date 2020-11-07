﻿using System;
using Microsoft.AspNetCore.Builder;

namespace EloMatches.Api.Application.Middleware.Exceptions
{
    public static class ApiExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder)
        {
            var options = new ApiExceptionOptions();
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }

        public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder builder, Action<ApiExceptionOptions> configureOptions)
        {
            var options = new ApiExceptionOptions();
            configureOptions(options);
            return builder.UseMiddleware<ApiExceptionMiddleware>(options);
        }
    }
}