namespace Elwin.GoGroceries.Domain.Models;

public class Product : NamedEntity
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct>? ListProducts { get; private set; }

    public Product(string name) : base(name)
    {

    }

    public void AddListProduct(GroceryListProduct listProduct)
    {
        _listProducts.Add(listProduct);
    }
}
