using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyResourceFilter : IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - After - MyResourceFilter");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyResourceFilter");
        }
    }
}
