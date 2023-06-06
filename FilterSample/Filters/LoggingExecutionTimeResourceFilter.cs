using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class LoggingExecutionTimeResourceFilter : IAsyncResourceFilter
    {
        private readonly ILogger<LoggingExecutionTimeResourceFilter> _logger;

        public LoggingExecutionTimeResourceFilter(ILogger<LoggingExecutionTimeResourceFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var start = DateTime.Now;
            await next();
            var requestTime = (DateTime.Now - start).TotalMilliseconds;
            _logger.LogInformation($"Request start at {start:HH:mm:ss:ffff} took {requestTime} ms");
        }
    }
}
