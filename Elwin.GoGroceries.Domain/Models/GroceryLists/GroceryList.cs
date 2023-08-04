using Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListTemplates;

namespace Elwin.GoGroceries.Domain.Models.GroceryLists;

public class GroceryList : GroceryListBase
{
    private readonly List<GroceryListProduct> _listProducts = new();
    public virtual IReadOnlyCollection<GroceryListProduct> ListProducts => _listProducts;

    public GroceryList(string name) : base(name)
    {

    }

    public GroceryListTemplate ToTemplate()
    {
        var template = new GroceryListTemplate(Name);

        foreach (var product in ListProducts)
        {
            template.AddProduct(product);
        }

        return template;
    }
}
