using Supermarket.Domain.Models;

namespace Supermarket.Domain.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> ListAsync();
    }
}
