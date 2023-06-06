using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyAsyncFilterAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _caller;

        public MyAsyncFilterAttribute(string caller)
        {
            _caller = caller;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyAsyncFilterAttribute - {_caller}");
            await next();
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - After - MyAsyncFilterAttribute - {_caller}");
        }
    }
}
