using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Contracts.Post;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.Mappers;
using Elwin.GoGroceries.Infrastructure.Repositories;

namespace Elwin.GoGroceries.Core.Managers;

public interface IManageGroceryLists
{
    Task<GroceryListDto> GetGroceryListAsync(Guid id);
    Task<ICollection<GroceryListDto>> GetAllGroceryListsAsync();
    Task<bool> DoesListProductExistAsync(Guid listId, string name, Guid categoryId);
    Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto);
    Task<GroceryListDto> AddProductToListAsync(Guid listId, PostProductDto dto);
    Task<GroceryListDto> UpdateGroceryListProducts(GroceryListDto groceryListDto);
    Task<ProductDto> PutProductUpdate(Guid groceryListId, PostProductDto productDto);
    Task DeleteGroceryListAsync(Guid listId);
    Task<GroceryListDto> RemoveProductFromGroceryListAsync(Guid listId, Guid productId);
}

public class ManageGroceryLists : IManageGroceryLists
{
    private readonly IGroceryRepository _groceryRepository;
    private readonly ICategoryRepository _categoryRepostiroy;

    public ManageGroceryLists(IGroceryRepository groceryRepository, ICategoryRepository categoryRepostiroy)
    {
        _groceryRepository = groceryRepository;
        _categoryRepostiroy = categoryRepostiroy;
    }

    public async Task<GroceryListDto> GetGroceryListAsync(Guid id)
    {
        var groceryList = await _groceryRepository.FindAsync(id);
        return GroceryListMapper.ToDto(groceryList);
    }

    public async Task<ICollection<GroceryListDto>> GetAllGroceryListsAsync()
    {
        var groceryLists = await _groceryRepository.GetAll();
        return groceryLists.Select(GroceryListMapper.ToDto).ToList();
    }

    public async Task<bool> DoesListProductExistAsync(Guid listId, string name, Guid categoryId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);
        return groceryList.ValidateProductNotDuplicate(name, categoryId);
    }

    public async Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto)
    {
        dto.Name = dto.Name.Capitalize();
        var res = await _groceryRepository.AddAsync(new GroceryList(dto.Name));

        return GroceryListMapper.ToDto(res);
    }

    public async Task<GroceryListDto> AddProductToListAsync(Guid listId, PostProductDto dto)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);

        if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));

        // Consider refactoring below to Chain of Responsibilities design pattern in the future
        Guid categoryId;

        if (dto.Category is null || dto.Category.Id == Guid.Empty)
        {
            dto.Category.Name = dto.Category.Name.Capitalize();
            var category = await _categoryRepostiroy.AddAsync(new Category(dto.Category.Name, dto.Category.ColorCode));
            categoryId = category.Id;
        }
        else
        {
            categoryId = dto.Category.Id;
        }

        var product = await _groceryRepository.FindItemByNameAsync(dto.Name);

        if (product is null)
        {
            dto.Name = dto.Name.Capitalize();

            await _groceryRepository.AddProductAsync(groceryList, new Product(dto.Name), dto.Category.Id, dto.Quantity, dto.Measurement, dto.MeasurementQuantity);
        }
        else
        {
            await _groceryRepository.AddProductAsync(groceryList, product, dto.Category.Id, dto.Quantity, dto.Measurement, dto.MeasurementQuantity);
        }

        return GroceryListMapper.ToDto(groceryList);
    }

    public async Task<GroceryListDto> UpdateGroceryListProducts(GroceryListDto groceryListDto)
    {
        var groceryList = await _groceryRepository.FindAsync(groceryListDto.Id);

        foreach(var productDto in groceryListDto.Products)
        {
            var product = groceryList.ListProducts.SingleOrDefault(lp => lp.Product.Id == productDto.Id);
            if (product is not null) await _groceryRepository.UpdateProductAsync(groceryList, product.Id, productDto.IsCheckedOff);
        }

        return GroceryListMapper.ToDto(groceryList);
    }

    public async Task<ProductDto> PutProductUpdate(Guid groceryListId, PostProductDto productDto)
    {
        var groceryList = await _groceryRepository.FindAsync(groceryListId);
        var listProduct = groceryList.ListProducts.Single(lp => lp.Product.Id == productDto.Id);

        if (listProduct.Product.Name != productDto.Name)
        {
            var product = await _groceryRepository.FindItemByNameAsync(productDto.Name);

            if (product is null)
            {
                productDto.Name = productDto.Name.Capitalize();
                var res = await _groceryRepository.AddProductAsync(groceryList, new Product(productDto.Name), productDto.Category.Id, productDto.Quantity, productDto.Measurement, productDto.MeasurementQuantity);
                listProduct = res.ListProducts.Single(lp => lp.Id == listProduct.Id);
            }
            else
            {
                var res = await _groceryRepository.AddProductAsync(groceryList, product, productDto.Category.Id, productDto.Quantity, productDto.Measurement, productDto.MeasurementQuantity);
                listProduct = res.ListProducts.Single(lp => lp.Id == listProduct.Id);
            }

            await _groceryRepository.RemoveProductAsync(groceryList ,listProduct);

        }
        else
        {
            await _groceryRepository.UpdateProductAsync(groceryList, listProduct.Id, productDto.Category.Id, productDto.Quantity, productDto.Measurement, productDto.MeasurementQuantity);
        }

        return ProductMapper.ToDto(listProduct);
    }

    public async Task DeleteGroceryListAsync(Guid listId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);
        await _groceryRepository.DeleteAsync(groceryList);
    }

    public async Task<GroceryListDto> RemoveProductFromGroceryListAsync(Guid listId, Guid productId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);

        var listProduct = groceryList.ListProducts.Single(i => i.ProductId == productId);
        var res = await _groceryRepository.RemoveProductAsync(groceryList, listProduct);
        return GroceryListMapper.ToDto(res);
    }
}
