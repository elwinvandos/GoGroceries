using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class TemplateMapper
    {
        public static TemplateDto ToDto(GroceryListTemplate template)
        {
            return new TemplateDto()
            {
                Id = template.Id,
                Products = template.TemplateProducts.Select(ProductMapper.ToDto).ToList(),
                Name = template.Name
            };
        }
    }
}
