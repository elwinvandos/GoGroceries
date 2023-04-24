namespace Elwin.GoGroceries.Domain.Models;

public class GroceryList : Entity
{
    public string Name { get; private set; }

    private readonly List<GroceryItem> _groceryItems = new();
    public IReadOnlyCollection<GroceryItem> GroceryItems => _groceryItems;

    public GroceryList(string name)
    {
        Name = name;
    }

    public void AddGroceryItem(GroceryItem item)
    {
        //todo: validation
        _groceryItems.Add(item);
    }

    public void DeleteGroceryItem(GroceryItem item)
    {
        //todo: validation
        _groceryItems.Remove(item);
    }

    public void ClearGroceryItems()
    {
        _groceryItems.Clear();
    }
}
