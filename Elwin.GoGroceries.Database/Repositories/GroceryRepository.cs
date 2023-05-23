using Elwin.GoGroceries.Infrastructure.DbContexts;
using Elwin.GoGroceries.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Elwin.GoGroceries.Infrastructure.Repositories;

public interface IGroceryRepository
{
    Task<GroceryList> FindAsync(Guid groceryListId);
    Task<Product> FindItemByNameAsync(string name);
    Task<ICollection<GroceryList>> GetAll();
    Task<ICollection<Product>> GetAllProducts();
    Task<GroceryList> AddAsync(GroceryList groceryList);
    Task<GroceryList> AddProductAsync(GroceryList groceryList, Product groceryItem, int? quantity, string? measurement, int? measurementQuantity);
    Task DeleteAsync(GroceryList groceryList);
    Task<GroceryList> RemoveProductAsync(GroceryList groceryList, GroceryListProduct listProduct);
    Task<GroceryListProduct> PutProductAssignmentAsync(GroceryList groceryList, Guid productId);
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

    public async Task<ICollection<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<GroceryList> FindAsync(Guid listId)
    {
        return await _context.GroceryLists
            .Include(gl => gl.ListProducts)
                .ThenInclude(lp => lp.Product)
            .SingleOrDefaultAsync(gl => gl.Id == listId);
    }

    public async Task<Product> FindItemByNameAsync(string name)
    {
        return await _context.Products
            .SingleOrDefaultAsync(i => i.Name == name);
    }

    public async Task<GroceryList> AddAsync(GroceryList groceryList)
    {
        var res = await _context.GroceryLists.AddAsync(groceryList);
        await _context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<GroceryList> AddProductAsync(GroceryList groceryList, Product product, int? quantity, string? measurement, int? measurementQuantity)
    {
        groceryList.AddProduct(product, quantity, measurement, measurementQuantity);
        await _context.SaveChangesAsync();
        return groceryList;
    }

    public async Task<GroceryListProduct> PutProductAssignmentAsync(GroceryList groceryList, Guid productId)
    {
        var listProduct = groceryList.ListProducts.Single(lp => lp.ProductId == productId);
        listProduct.ToggleIsCheckedOff();
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
