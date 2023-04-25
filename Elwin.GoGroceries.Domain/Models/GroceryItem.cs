namespace Elwin.GoGroceries.Domain.Models;

public class GroceryItem : Entity
{
    public string Name { get; private set; }
    public int? Quantity { get; private set; }
    public int? Weight { get; private set; }

    public Guid? CategoryId { get; private set; }

    public GroceryItem(string name, Guid? categoryId, int? quantity = null, int? weight = null)
    {
        Name = name;
        CategoryId = categoryId;
        Quantity = quantity;
        Weight = weight;
    }
}
