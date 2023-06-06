using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace RateLimit
{
    public class SampleRateLimiterPolicy : IRateLimiterPolicy<string>
    {
        public Func<OnRejectedContext, CancellationToken, ValueTask>? OnRejected { get; }
            = (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                return new();
            };

        public RateLimitPartition<string> GetPartition(HttpContext httpContext)
        {

            return RateLimitPartition.GetFixedWindowLimiter(string.Empty, options => new()
            {
                PermitLimit = 5,
                Window = TimeSpan.FromHours(1)
            });
        }
    }
}
