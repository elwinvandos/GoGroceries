using Elwin.GoGroceries.Domain.Models.GroceryLists.GroceryListTemplates;

namespace Elwin.GoGroceries.Domain.Models.GroceryListTemplates;

public class GroceryListTemplateProduct : GroceryListProductBase
{
    public Guid GroceryListTemplateId { get; private set; }
    public GroceryListTemplate GroceryListTemplate { get; private set; }
}
