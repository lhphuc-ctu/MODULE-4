using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly string _caller;

        public MyActionFilterAttribute(string caller)
        {
            _caller = caller;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - After - MyActionFilterAttribute - {_caller}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyActionFilterAttribute - {_caller}");
        }
    }
}
