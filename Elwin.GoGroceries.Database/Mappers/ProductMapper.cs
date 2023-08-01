using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(GroceryListProduct listProduct)
        {
            return new ProductDto()
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
    }
}
