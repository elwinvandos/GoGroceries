using Elwin.GoGroceries.Contracts.Product;
using Elwin.GoGroceries.Domain.Models.GroceryLists;
using Elwin.GoGroceries.Domain.Models.GroceryLists.Templates;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ListProductDto ToDto(GroceryListProduct listProduct)
        {
            return new ListProductDto()
            {
                Id = listProduct.Product.Id,
                Name = listProduct.Product.Name,
                Measurement = listProduct.Measurement,
                MeasurementQuantity= listProduct.MeasurementQuantity,
                Quantity = listProduct.Quantity,
                CategoryId = listProduct.CategoryId,
                IsCheckedOff = listProduct.IsCheckedOff,
            };
        }

        public static TemplateProductDto ToDto(GroceryListTemplateProduct templateProduct)
        {
            return new TemplateProductDto()
            {
                Id = templateProduct.Product.Id,
                Name = templateProduct.Product.Name,
                Measurement = templateProduct.Measurement,
                MeasurementQuantity = templateProduct.MeasurementQuantity,
                Quantity = templateProduct.Quantity,
                CategoryId = templateProduct.CategoryId
            };
        }
    }
}
