using Elwin.GoGroceries.Domain.Models.Base;

namespace Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;

public class GroceryListTemplateProduct : Entity
{
    public Guid GroceryListTemplateId { get; private set; }
    public GroceryListTemplate GroceryListTemplate { get; private set; }

    public Guid ProductId { get; private set; }
    public Product Product { get; private set; }

    public Guid CategoryId { get; private set; }
    public int? Quantity { get; private set; }
    public string? Measurement { get; private set; }
    public int? MeasurementQuantity { get; private set; }

    private GroceryListTemplateProduct()
    {
    }

    public GroceryListTemplateProduct(GroceryListTemplate groceryListTemplate, Product product, Guid categoryId, int? quantity = null, string? measurement = null, int? measurementQuantity = null)
    {
        GroceryListTemplate = groceryListTemplate ?? throw new ArgumentNullException(nameof(groceryListTemplate));
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
