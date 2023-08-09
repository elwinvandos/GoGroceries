using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;

namespace Elwin.GoGroceries.Domain.Models;

public class Product : NamedEntity
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct>? ListProducts => _listProducts;


    private readonly List<GroceryListTemplateProduct> _templateProducts = new();
    public virtual IReadOnlyCollection<GroceryListTemplateProduct>? TemplateProducts => _templateProducts;

    public Product(string name) : base(name)
    {

    }

    public void AddListProduct(GroceryListProduct listProduct)
    {
        _listProducts.Add(listProduct);
    }

    public void AddTemplateProduct(GroceryListTemplateProduct templateProduct)
    {
        _templateProducts.Add(templateProduct);
    }
}
