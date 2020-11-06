using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EloMatches.Api.Application.Middleware.Exceptions
{
    public class ApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiExceptionMiddleware> _logger;
        private readonly ApiExceptionOptions _options;

        public ApiExceptionMiddleware(RequestDelegate next, ILogger<ApiExceptionMiddleware> logger, ApiExceptionOptions options)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var error = new ApiError
            {
                Id = Guid.NewGuid().ToString(),
                Status = (short)HttpStatusCode.InternalServerError,
                Title = "Error occured in the API. Use the id and contact our administrators if the error persists."
            };

            _options.AddResponseDetails?.Invoke(context, exception, error);

            var exceptionMessage = GetInnermostException(exception);
            _logger.LogError(exception, "Error occured: {ExceptionMessage} -- ErrorId = {ErrorId}", exceptionMessage, error.Id);

            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }

        private static string GetInnermostException(Exception exception)
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