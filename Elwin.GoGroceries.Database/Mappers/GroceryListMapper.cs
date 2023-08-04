using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models.GroceryLists;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class GroceryListMapper
    {
        public static GroceryListDto ToDto(GroceryList groceryList)
        {
            return new GroceryListDto()
            {
                Id = groceryList.Id,
                Products = groceryList.ListProducts.Select(ProductMapper.ToDto).ToList(),
                Name = groceryList.Name
            };
        }
    }
}
