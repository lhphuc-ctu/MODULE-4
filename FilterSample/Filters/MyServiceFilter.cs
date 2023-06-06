using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyServiceFilter : IResultFilter
    {
        private readonly ILogger<MyServiceFilter> _logger;

        public MyServiceFilter(ILogger<MyServiceFilter> logger)
        {
            _logger = logger;
        }
        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss:ffff} - After - MyServiceFilter");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyServiceFilter");
        }
    }
}
