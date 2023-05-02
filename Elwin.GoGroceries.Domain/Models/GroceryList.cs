namespace Elwin.GoGroceries.Domain.Models;

public class GroceryList : Entity
{
    public string Name { get; private set; }

    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct> ListProducts => _listProducts;

    public GroceryList(string name)
    {
        Name = name;
    }

    public void AddProduct(Product product)
    {
        //todo: validation
        var listProduct = new GroceryListProduct(this, product);
        _listProducts.Add(listProduct);
        product.AddListProduct(listProduct);
    }

    public void RemoveProduct(GroceryListProduct listProduct)
    {
        //todo: validation
        _listProducts.Remove(listProduct);
    }

    public void ClearProducts()
    {
        _listProducts.Clear();
    }
}
