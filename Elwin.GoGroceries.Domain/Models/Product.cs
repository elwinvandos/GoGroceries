namespace Elwin.GoGroceries.Domain.Models;

public class Product : Entity
{
    public string Name { get; private set; }
    public int? Quantity { get; private set; }
    public string? Measurement { get; private set; }
    public int? MeasurementQuantity { get; private set; }

    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct>? ListProducts { get; private set; }
    public Guid CategoryId { get; private set; }

    public Product(string name, Guid categoryId, int? quantity = null, string? measurement = null, int? measurementQuantity = null)
    {
        Name = name;
        CategoryId = categoryId;
        Quantity = quantity;
        Measurement = measurement;
        MeasurementQuantity = measurementQuantity;
    }

    public void AddListProduct(GroceryListProduct listProduct)
    {
        _listProducts.Add(listProduct);
    }
}
