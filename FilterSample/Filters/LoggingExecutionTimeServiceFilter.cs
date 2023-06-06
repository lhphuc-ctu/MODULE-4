using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class LoggingExecutionTimeServiceFilter : IAsyncResultFilter
    {
        private readonly ILogger<LoggingExecutionTimeServiceFilter> _logger;

        public LoggingExecutionTimeServiceFilter(ILogger<LoggingExecutionTimeServiceFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var start = DateTime.Now;
            await next();
            var requestTime = (DateTime.Now - start).TotalMilliseconds;

            _logger.LogInformation($"Request start at {start:HH:mm:ss:ffff} took {requestTime} ms");
        }
    }
}
