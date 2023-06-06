namespace ResponeCaching
{
    public class CustomMiddleware
    {
        private readonly ILogger<CustomMiddleware> _logger;
        private readonly RequestDelegate _next;

        public CustomMiddleware(ILogger<CustomMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            _logger.LogInformation($"{context.Request.Method} {context.Request.Path}");
        }
    }

    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomMiddleware>();
        }
    }
}
