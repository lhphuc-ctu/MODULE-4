namespace Middleware
{
    public class TimingMiddleware
    {
        private readonly ILogger<TimingMiddleware> _logger;
        private readonly RequestDelegate _next;

        public TimingMiddleware(ILogger<TimingMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var start = DateTime.Now;

            await _next(context);

            var requestTime =(DateTime.Now - start).TotalMilliseconds;

            _logger.LogInformation($"Request took {requestTime} ms: {context.Request.Method} {context.Request.Path}");

        }
    }

    public static class TimingExtensions
    {
        public static IApplicationBuilder UseTiming(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TimingMiddleware>();
        }
    }
}
