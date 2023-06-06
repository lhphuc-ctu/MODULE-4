﻿namespace FilterSample.Middleware
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

            var requestTime = (DateTime.Now - start).TotalMilliseconds;

            _logger.LogInformation($"Request start at {start:HH:mm:ss:ffff} took {requestTime} ms");

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
