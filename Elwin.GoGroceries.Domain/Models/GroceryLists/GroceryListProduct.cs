namespace Elwin.GoGroceries.Domain.Models.GroceryLists;

public class GroceryListProduct : GroceryListProductBase
{
    public Guid GroceryListId { get; private set; }
    public GroceryListBase GroceryList { get; private set; }

    public bool IsCheckedOff { get; private set; } = false;

    public GroceryListProduct(GroceryListBase groceryList, Product product, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
        : base(product, categoryId, quantity, measurement, measurementQuantity)
    {
        GroceryList = groceryList ?? throw new ArgumentNullException(nameof(groceryList));
    }

    public void ToggleIsCheckedOff()
    {
        IsCheckedOff = !IsCheckedOff;
    }

    public void Update(bool isCheckedOff)
    {
        IsCheckedOff = isCheckedOff;
    }
}
