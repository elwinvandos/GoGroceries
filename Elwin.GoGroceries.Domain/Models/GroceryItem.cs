namespace Elwin.GoGroceries.Domain.Models;

public class GroceryItem : Entity
{
    public string Name { get; private set; }
    public Guid? CategoryId { get; private set; }

    public GroceryItem(string name, Guid? categoryId)
    {
        Name = name;
        CategoryId = categoryId;
    }
}
