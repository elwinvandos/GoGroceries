namespace Elwin.GoGroceries.Domain.Models;

public class GroceryList : NamedEntity
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct> ListProducts => _listProducts;

    public GroceryList(string name) : base(name)
    {

    }

    public void AddProduct(Product product, Guid categoryId, int? quantity, string? measurement, int? measurementQuantity)
    {
        var listProduct = new GroceryListProduct(this, product, categoryId, quantity, measurement, measurementQuantity);
        _listProducts.Add(listProduct);
        product.AddListProduct(listProduct);
    }

    public bool RemoveProduct(GroceryListProduct listProduct)
    {
        return _listProducts.Remove(listProduct);
    } 

    public void ClearProducts()
    {
        _listProducts.Clear();
    }

    public bool ValidateProductNotDuplicate(string name, Guid categoryId)
    {
        return _listProducts.Any(lp => lp.Product.Name == name && lp.CategoryId == categoryId);
    }
}
