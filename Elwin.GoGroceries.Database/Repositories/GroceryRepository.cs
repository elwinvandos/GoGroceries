using Elwin.GoGroceries.Infrastructure.DbContexts;
using Elwin.GoGroceries.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Elwin.GoGroceries.Infrastructure.Repositories;

public interface IGroceryRepository
{
    Task<GroceryList> FindAsync(Guid groceryListId);
    Task<ICollection<GroceryList>> GetAll();
    Task<GroceryList> AddAsync(GroceryList groceryList);
    Task<GroceryList> AddGroceryItemAsync(GroceryList groceryList, GroceryItem groceryItem);
    Task DeleteAsync(GroceryList groceryList);
    Task<GroceryList> DeleteGroceryItemAsync(GroceryList groceryList, GroceryItem groceryItem);
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

    public async Task<GroceryList> FindAsync(Guid groceryListId)
    {
        var bla = await _context.GroceryLists
            .Include(gl => gl.GroceryItems)
            .SingleOrDefaultAsync(gl => gl.Id == groceryListId) ?? throw new ArgumentNullException(nameof(groceryListId));

        return await _context.GroceryLists
            .Include(gl => gl.GroceryItems)
            .SingleOrDefaultAsync(gl => gl.Id == groceryListId) ?? throw new ArgumentNullException(nameof(groceryListId));
    }

    public async Task<GroceryList> AddAsync(GroceryList groceryList)
    {
        var res = await _context.GroceryLists.AddAsync(groceryList);
        await _context.SaveChangesAsync();
        return res.Entity;
    }

    public async Task<GroceryList> AddGroceryItemAsync(GroceryList groceryList, GroceryItem groceryItem)
    {
        groceryList.AddGroceryItem(groceryItem);
        await _context.SaveChangesAsync();
        return groceryList;
    }

    public async Task DeleteAsync(GroceryList groceryList)
    {
        groceryList.ClearGroceryItems();
        _context.GroceryLists.Remove(groceryList);
        await _context.SaveChangesAsync();
    }

    public async Task<GroceryList> DeleteGroceryItemAsync(GroceryList groceryList, GroceryItem groceryItem)
    {
        groceryList.DeleteGroceryItem(groceryItem);
        await _context.SaveChangesAsync();
        return groceryList;
    }
}
