using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models;

namespace Elwin.GoGroceries.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(Product product)
        {
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Weight = product.Weight,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
            };
        }

        public static ProductDto ToDto(GroceryListProduct listProduct)
        {
            return new ProductDto()
            {
                Id = listProduct.Product.Id,
                Name = listProduct.Product.Name,
                Weight = listProduct.Product.Weight,
                Quantity = listProduct.Product.Quantity,
                CategoryId = listProduct.Product.CategoryId,
                IsCheckedOff = listProduct.IsCheckedOff
            };
        }
    }
}
