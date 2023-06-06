using Microsoft.EntityFrameworkCore;
using Supermarket.Domain.Models;
using Supermarket.Domain.Repositories;
using Supermarket.Persistence.Contexts;

namespace Supermarket.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<Category> FindByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public void Remove(Category category)
        {
            _context.Categories.Remove(category);
        }

        public void UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
        }
    }
}
