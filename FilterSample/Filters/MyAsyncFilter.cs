using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyAsyncFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyAsyncFilter");
            await next();
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - After - MyAsyncFilter");
        }
    }
}
