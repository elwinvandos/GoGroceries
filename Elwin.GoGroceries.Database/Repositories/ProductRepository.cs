using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Elwin.GoGroceries.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<Product> FindProductByNameAsync(string name);
        Task<ICollection<Product>> GetAllProducts();
        Task IncreaseUserWeight(Product product);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly GroceriesContext _context;

        public ProductRepository(GroceriesContext context)
        {
            _context = context;
        }

        public async Task<Product> FindProductByNameAsync(string name)
        {
            return await _context.Products
                .SingleOrDefaultAsync(i => i.Name == name);
        }

        public async Task<ICollection<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task IncreaseUserWeight(Product product)
        {
            product.IncreaseUserWeight();
            await _context.SaveChangesAsync();
        }
    }
}
