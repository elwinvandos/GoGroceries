using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;
using Elwin.GoGroceries.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Elwin.GoGroceries.Infrastructure.Repositories
{
    public interface ITemplateRepository
    {
        Task<ICollection<GroceryListTemplate>> GetAll();
        Task<GroceryListTemplate> FindAsync(Guid listId);
        Task<GroceryListTemplate> AddAsync(GroceryListTemplate template);
        Task DeleteAsync(GroceryListTemplate template);
        Task<GroceryListTemplate> AddProductAsync(GroceryListTemplate template, Product product, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity);
        Task<GroceryListTemplateProduct> UpdateProductAsync(GroceryListTemplate template, Guid productId, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity);
        Task<GroceryListTemplate> RemoveProductAsync(GroceryListTemplate template, GroceryListTemplateProduct templateProduct);
    }

    public class TemplateRepository : ITemplateRepository
    {
        private readonly GroceriesContext _context;

        public TemplateRepository(GroceriesContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GroceryListTemplate>> GetAll()
        {
            return await _context.GroceryListTemplates.ToListAsync();
        }

        public async Task<GroceryListTemplate> FindAsync(Guid templateId)
        {
            return await _context.GroceryListTemplates
                .Include(gl => gl.TemplateProducts)
                    .ThenInclude(lp => lp.Product)
                .SingleOrDefaultAsync(gl => gl.Id == templateId) ?? throw new ArgumentNullException(nameof(templateId));
        }

        public async Task<GroceryListTemplate> AddAsync(GroceryListTemplate template)
        {
            var res = await _context.GroceryListTemplates.AddAsync(template);
            await _context.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<GroceryListTemplate> AddProductAsync(GroceryListTemplate template, Product product, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
        {
            template.AddProduct(product, categoryId, quantity, measurement, measurementQuantity);
            await _context.SaveChangesAsync();
            return template;
        }
        public async Task<GroceryListTemplateProduct> UpdateProductAsync(GroceryListTemplate template, Guid productId, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
        {
            var templateProduct = template.TemplateProducts.Single(lp => lp.Id == productId);
            templateProduct.Update(categoryId, quantity, measurement, measurementQuantity);
            await _context.SaveChangesAsync();
            return templateProduct;
        }

        public async Task DeleteAsync(GroceryListTemplate template)
        {
            template.ClearProducts();
            _context.GroceryListTemplates.Remove(template);
            await _context.SaveChangesAsync();
        }

        public async Task<GroceryListTemplate> RemoveProductAsync(GroceryListTemplate template, GroceryListTemplateProduct templateProduct)
        {
            template.RemoveProduct(templateProduct);
            await _context.SaveChangesAsync();
            return template;
        }
    }
}
