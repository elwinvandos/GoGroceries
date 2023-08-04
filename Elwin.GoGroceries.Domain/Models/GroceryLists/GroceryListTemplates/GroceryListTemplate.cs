using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Elwin.GoGroceries.Domain.Models.GroceryListTemplates;

namespace Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListTemplates;

public class GroceryListTemplate : GroceryListBase
{
    private readonly List<GroceryListTemplateProduct> _listTemplateProducts = new();
    public virtual IReadOnlyCollection<GroceryListTemplateProduct> ListTemplateProducts => _listTemplateProducts;

    public GroceryListTemplate(string name) : base(name)
    {
    }

    public GroceryList ToGroceryList()
    {
        var groceryList = new GroceryList(Name);

        foreach (var product in ListProducts)
        {
            groceryList.AddProduct(product);
        }

        return groceryList;
    }
}
