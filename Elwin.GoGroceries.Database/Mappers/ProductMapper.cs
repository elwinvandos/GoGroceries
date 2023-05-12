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
                Measurement = listProduct.Product.Measurement,
                MeasurementQuantity= listProduct.Product.MeasurementQuantity,
                Quantity = listProduct.Product.Quantity,
                CategoryId = listProduct.Product.CategoryId,
                IsCheckedOff = listProduct.IsCheckedOff,
            };
        }
    }
}
