using System;
using Microsoft.AspNetCore.Http;

namespace EloMatches.Api.Infrastructure.CorrelationIds
{
    public class CorrelationIdAccessorFromHttpContext : ICorrelationIdAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CorrelationIdAccessorFromHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid GetCorrelationId()
        {
            return new Guid(_httpContextAccessor.HttpContext.TraceIdentifier);
        }
    }
}