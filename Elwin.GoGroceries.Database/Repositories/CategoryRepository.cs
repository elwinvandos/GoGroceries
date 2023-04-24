using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Elwin.GoGroceries.Infrastructure.Repositories
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAll();
        Task<Category> FindAsync(Guid categoryId);
        Task<Category> AddAsync(Category category);
        Task DeleteAsync(Category category);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly GroceriesContext _context;

        public CategoryRepository(GroceriesContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Category>> GetAll()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> FindAsync(Guid categoryId)
        {
            return await _context.Categories
                .SingleOrDefaultAsync(gl => gl.Id == categoryId) ?? throw new ArgumentNullException(nameof(categoryId));
        }

        public async Task<Category> AddAsync(Category category)
        {
            var res = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
