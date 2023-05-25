namespace Elwin.GoGroceries.Domain.Models;

public class GroceryList : NamedEntity
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct> ListProducts => _listProducts;

    public GroceryList(string name) : base(name)
    {

    }

    public void AddProduct(Product product, int? quantity, string? measurement, int? measurementQuantity)
    {
        var listProduct = new GroceryListProduct(this, product, quantity, measurement, measurementQuantity);
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

    public bool ValidateProductNotDuplicate(string name)
    {
        return ListProducts.Any(lp => lp.Product.Name == name);
    }
}
