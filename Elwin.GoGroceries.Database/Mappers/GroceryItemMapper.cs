using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class GroceryItemMapper
    {
        public static GroceryItemDto ToDto(GroceryItem item)
        {
            return new GroceryItemDto()
            {
                Id = item.Id,
                Name = item.Name,
                Weight = item.Weight,
                Quantity = item.Quantity,
                CategoryId = item.CategoryId,
            };
        }
    }
}
