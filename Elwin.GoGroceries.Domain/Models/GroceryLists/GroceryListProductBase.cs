namespace Elwin.GoGroceries.Domain.Models;

public class GroceryListProductBase : Entity
{
    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid CategoryId { get; private set; }
    public int? Quantity { get; private set; }
    public string? Measurement { get; private set; }
    public int? MeasurementQuantity { get; private set; }

    public GroceryListProductBase()
    {
    }

    public GroceryListProductBase(Product product, Guid categoryId, int? quantity = null, string? measurement = null, int? measurementQuantity = null)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
        CategoryId = categoryId;
        Quantity = quantity;
        Measurement = measurement;
        MeasurementQuantity = measurementQuantity;
    }

    public void Update(Guid categoryId, int? quantity = null, string? measurement = null, int? measureMentQuantity = null)
    {
        CategoryId = categoryId;
        Quantity = quantity;
        Measurement = measurement;
        MeasurementQuantity = measureMentQuantity;
    }
}
