using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class GroceryListMapper
    {
        public static GroceryListDto ToDto(GroceryList groceryList)
        {
            return new GroceryListDto()
            {
                Id = groceryList.Id,
                GroceryItems = groceryList.GroceryItems.Select((i) => new GroceryItemDto() { Id = i.Id, Name = i.Name, CategoryId = i.CategoryId }).ToList(),
                Name = groceryList.Name
            };
        }
    }
}
