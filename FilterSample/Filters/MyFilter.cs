using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyFilter");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - After - MyFilter");
        }
    }
}
