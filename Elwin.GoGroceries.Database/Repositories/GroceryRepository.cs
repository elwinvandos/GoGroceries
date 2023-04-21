using Elwin.GoGroceries.Infrastructure.DbContexts;
using Elwin.GoGroceries.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Elwin.GoGroceries.Infrastructure.Repositories;

public interface IGroceryRepository
{
    Task<GroceryList> FindAsync(Guid groceryListId);
    Task<ICollection<GroceryList>> GetAll();
    GroceryList Add(GroceryList groceryList);
    Task<GroceryList> AddGroceryItemAsync(GroceryList groceryList, GroceryItem groceryItem);
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
        return await _context.GroceryLists
            .Include(gl => gl.GroceryItems)
            .SingleOrDefaultAsync(gl => gl.Id == groceryListId) ?? throw new ArgumentNullException(nameof(groceryListId));
    }

    public GroceryList Add(GroceryList groceryList)
    {
        var entity = _context.GroceryLists.Add(groceryList).Entity;
        _context.SaveChanges();
        return entity;
    }

    public async Task<GroceryList> AddGroceryItemAsync(GroceryList groceryList, GroceryItem groceryItem)
    {
        groceryList.AddGroceryItem(groceryItem);
        await _context.SaveChangesAsync();
        return groceryList;
    }
}
