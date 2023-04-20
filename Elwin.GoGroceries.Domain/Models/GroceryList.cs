using Elwin.GoGroceries.Contracts;

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

    public static GroceryListDto ToDto(GroceryList groceryList)
    {
        return new GroceryListDto()
        {
            Id = groceryList.Id,
            ShoppingItems = groceryList.GroceryItems.Select((x) => new GroceryItemDto() { Name = x.Name }),
            Name = groceryList.Name
        };
    }
}
