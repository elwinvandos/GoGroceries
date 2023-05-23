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
    Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto);
    Task<GroceryListDto> AddProductToListAsync(Guid listId, PostProductDto dto);
    Task DeleteGroceryListAsync(Guid listId);
    Task<GroceryListDto> RemoveProductFromGroceryListAsync(Guid listId, Guid productId);
    Task<ProductDto> PutProductAssignmentAsync(Guid groceryListId, Guid productId);
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

        //todo proper validation
        if (groceryList is null) throw new ArgumentNullException(nameof(id));

        return GroceryListMapper.ToDto(groceryList);
    }

    public async Task<ICollection<GroceryListDto>> GetAllGroceryListsAsync()
    {
        var groceryLists = await _groceryRepository.GetAll();
        return groceryLists.Select(GroceryListMapper.ToDto).ToList();
    }

    public async Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto)
    {
        //todo: proper validation
        if (string.IsNullOrEmpty(dto?.Name))
        {
            throw new ArgumentNullException(nameof(dto.Name));
        }

        dto.Name = dto.Name.Capitalize();
        var res = await _groceryRepository.AddAsync(new GroceryList(dto.Name));

        return GroceryListMapper.ToDto(res);
    }

    public async Task<GroceryListDto> AddProductToListAsync(Guid listId, PostProductDto dto)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);

        //todo proper validation & extract it
        if (groceryList is null) throw new ArgumentNullException(nameof(listId));
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

            await _groceryRepository.AddProductAsync(groceryList, new Product(dto.Name, categoryId, dto.Quantity, dto.Measurement, dto.MeasurementQuantity));
        }
        else
        {
            await _groceryRepository.AddProductAsync(groceryList, product);
        }

        return GroceryListMapper.ToDto(groceryList);
    }

    public async Task<ProductDto> PutProductAssignmentAsync(Guid groceryListId, Guid productId)
    {
        var groceryList = await _groceryRepository.FindAsync(groceryListId);
        if (groceryList is null) throw new ArgumentNullException(nameof(groceryListId));
        var res = await _groceryRepository.PutProductAssignmentAsync(groceryList, productId);
        return ProductMapper.ToDto(res);
    }

    public async Task DeleteGroceryListAsync(Guid listId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);
        if (groceryList is null) throw new ArgumentNullException(nameof(listId));

        await _groceryRepository.DeleteAsync(groceryList);
    }

    public async Task<GroceryListDto> RemoveProductFromGroceryListAsync(Guid listId, Guid productId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);
        if (groceryList is null) throw new ArgumentNullException(nameof(productId));

        var listProduct = groceryList.ListProducts.Single(i => i.ProductId == productId);
        var res = await _groceryRepository.RemoveProductAsync(groceryList, listProduct);
        return GroceryListMapper.ToDto(res);
    }
}
