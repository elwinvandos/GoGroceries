using Elwin.GoGroceries.Infrastructure.DbContexts;
using Elwin.GoGroceries.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Elwin.GoGroceries.Domain.Models.GroceryLists;

namespace Elwin.GoGroceries.Infrastructure.Repositories;

public interface IGroceryRepository
{
    Task<GroceryList> FindAsync(Guid groceryListId);
    Task<ICollection<GroceryList>> GetAll();
    Task<GroceryList> AddAsync(GroceryList groceryList);
    Task<GroceryList> AddProductAsync(GroceryList groceryList, Product groceryItem, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity);
    Task DeleteAsync(GroceryList groceryList);
    Task<GroceryList> RemoveProductAsync(GroceryList groceryList, GroceryListProduct listProduct);
    Task<GroceryListProduct> UpdateProductAsync(GroceryList groceryList, Guid productId, bool isCheckedOff);
    Task<GroceryListProduct> UpdateProductAsync(GroceryList groceryList, Guid productId, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity);
}

public class GroceryRepository : IGroceryRepository
{
    private readonly GroceriesContext _context;

    public GroceryRepository(GroceriesContext context)
    {
        _context = context;
    }

    public async Task<ICollection<GroceryList>> GetAll()
    {
        return await _context.GroceryLists.ToListAsync();
    }

    public async Task<GroceryList> FindAsync(Guid listId)
    {
        return await _context.GroceryLists
            .Include(gl => gl.ListProducts)
                .ThenInclude(lp => lp.Product)
            .SingleOrDefaultAsync(gl => gl.Id == listId) ?? throw new ArgumentNullException(nameof(listId));
    }

    public async Task<GroceryList> AddAsync(GroceryList groceryList)
    {
        var res = await _context.GroceryLists.AddAsync(groceryList);
        await _context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<GroceryList> AddProductAsync(GroceryList groceryList, Product product, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
    {
        groceryList.AddProduct(product, categoryId, quantity, measurement, measurementQuantity);
        await _context.SaveChangesAsync();
        return groceryList;
    }

    public async Task<GroceryListProduct> UpdateProductAsync(GroceryList groceryList, Guid productId, bool isCheckedOff)
    {
        var listProduct = groceryList.ListProducts.Single(lp => lp.Id == productId);
        listProduct.Update(isCheckedOff);
        await _context.SaveChangesAsync();
        return listProduct;
    }

    public async Task<GroceryListProduct> UpdateProductAsync(GroceryList groceryList, Guid productId, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
    {
        var listProduct = groceryList.ListProducts.Single(lp => lp.Id == productId);
        listProduct.Update(categoryId, quantity, measurement, measurementQuantity);
        await _context.SaveChangesAsync();
        return listProduct;
    }

    public async Task DeleteAsync(GroceryList groceryList)
    {
        groceryList.ClearProducts();
        _context.GroceryLists.Remove(groceryList);
        await _context.SaveChangesAsync();
    }

    public async Task<GroceryList> RemoveProductAsync(GroceryList groceryList, GroceryListProduct listProduct)
    {
        groceryList.RemoveProduct(listProduct);
        await _context.SaveChangesAsync();
        return groceryList;
    }
}
