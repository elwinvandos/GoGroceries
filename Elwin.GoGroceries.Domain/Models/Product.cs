namespace Elwin.GoGroceries.Domain.Models;

public class Product : NamedEntity
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct>? ListProducts { get; private set; }
    public Guid CategoryId { get; private set; }

    public Product(string name, Guid categoryId) : base(name)
    {
        CategoryId = categoryId;
    }

    public void AddListProduct(GroceryListProduct listProduct)
    {
        _listProducts.Add(listProduct);
    }

    public void UpdateName(string name)
    {
        if (name == Name) return;
        Name = name;
    }
}
