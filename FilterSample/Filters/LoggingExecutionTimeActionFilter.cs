using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class LoggingExecutionTimeActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LoggingExecutionTimeActionFilter> _logger;

        public LoggingExecutionTimeActionFilter(ILogger<LoggingExecutionTimeActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var start = DateTime.Now;
            await next();
            var requestTime = (DateTime.Now - start).TotalMilliseconds;
            _logger.LogInformation($"Request start at {start:HH:mm:ss:ffff} took {requestTime} ms");
        }
    }
}
