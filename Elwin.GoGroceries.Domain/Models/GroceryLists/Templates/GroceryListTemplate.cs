namespace Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;

public class GroceryListTemplate : NamedEntity
{
    private readonly List<GroceryListTemplateProduct> _templateProducts = new();
    public virtual IReadOnlyCollection<GroceryListTemplateProduct> TemplateProducts => _templateProducts;

    public GroceryListTemplate(string name) : base(name)
    {

    }

    public void AddProduct(Product product, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
    {
        var templateProduct = new GroceryListTemplateProduct(this, product, categoryId, quantity, measurement, measurementQuantity);
        _templateProducts.Add(templateProduct);
        product.AddTemplateProduct(templateProduct);
    }

    public bool RemoveProduct(GroceryListTemplateProduct templateProduct)
    {
        return _templateProducts.Remove(templateProduct);
    }

    public void ClearProducts()
    {
        _templateProducts.Clear();
    }

    public bool ValidateProductNotDuplicate(string name, Guid categoryId)
    {
        return _templateProducts.Any(lp => lp.Product.Name == name && lp.CategoryId == categoryId);
    }

    public GroceryList ToGroceryList()
    {
        var groceryList = new GroceryList(this.Name);

        foreach (var templateProduct in _templateProducts)
        {
            groceryList.AddProduct(templateProduct.Product, templateProduct.CategoryId, templateProduct.Quantity, templateProduct.Measurement, templateProduct.MeasurementQuantity);
        }

        return groceryList;
    }
}
