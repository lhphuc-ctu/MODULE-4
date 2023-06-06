using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Supermarket.Extensions
{
    public static class ModelStateExtensions
    {
        public static List<string> GetErrorMessages(this ModelStateDictionary modelStateDictionary)
        {
            return modelStateDictionary.SelectMany(m=>m.Value!.Errors).Select(m=>m.ErrorMessage).ToList();
        }
    }
}
