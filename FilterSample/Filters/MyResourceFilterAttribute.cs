using Microsoft.AspNetCore.Mvc.Filters;

namespace FilterSample.Filters
{
    public class MyResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly string _caller;

        public MyResourceFilterAttribute(string caller)
        {
            _caller = caller;
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - After - MyResourceFilter - {_caller}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"{DateTime.Now:hh:mm:ss:ffff} - Before - MyResourceFilter - {_caller}");
        }
    }
}
