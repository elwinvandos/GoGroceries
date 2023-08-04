using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Elwin.GoGroceries.Domain.Models.GroceryListTemplates;

namespace Elwin.GoGroceries.Domain.Models;

public class Product : NamedEntity
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct>? ListProducts => _listProducts;

    private readonly List<GroceryListTemplateProduct> _listTemplateProducts = new();
    public virtual IReadOnlyCollection<GroceryListTemplateProduct>? ListTemplateProducts => _listTemplateProducts;

    public Product(string name) : base(name)
    {

    }

    public void AddListProduct(GroceryListProduct listProduct)
    {
        _listProducts.Add(listProduct);
    }
}
