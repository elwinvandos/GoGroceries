using Elwin.GoGroceries.Contracts;

namespace Elwin.GoGroceries.Domain.Models;

public class GroceryItem : Entity
{
    public string Name { get; private set; }

    public GroceryItem(string name)
    {
        Name = name;
    }

    public static GroceryItemDto ToDto(GroceryItem groceryItem)
    {
        return new GroceryItemDto()
        {
            Id = groceryItem.Id,
            Name = groceryItem.Name
        };
    }
}
