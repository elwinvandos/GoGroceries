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
    Task<ICollection<GroceryItemDto>> GetAllGroceryItemsAsync();
    Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto);
    Task<GroceryListDto> AddGroceryItemToListAsync(Guid listId, PostGroceryItemDto dto);
    Task DeleteGroceryListAsync(Guid listId);
    Task<GroceryListDto> DeleteGroceryItemFromListAsync(Guid listId, Guid itemId);
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

    public async Task<ICollection<GroceryItemDto>> GetAllGroceryItemsAsync()
    {
        var groceryItems = await _groceryRepository.GetAllGroceryItems();
        return groceryItems.Select(GroceryItemMapper.ToDto).ToList();
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

    public async Task<GroceryListDto> AddGroceryItemToListAsync(Guid listId, PostGroceryItemDto dto)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);

        //todo proper validation
        if (groceryList is null) throw new ArgumentNullException(nameof(listId));
        if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));

        dto.Name = dto.Name.Capitalize();

        if (dto.Category != null)
        {
            await _groceryRepository.AddGroceryItemAsync(groceryList, new GroceryItem(dto.Name, dto.Category.Id, dto.Quantity, dto.Weight));
        }
        else
        {
            dto.Category.Name = dto.Category.Name.Capitalize();
            var category = await _categoryRepostiroy.AddAsync(new Category(dto.Category.Name));
            await _groceryRepository.AddGroceryItemAsync(groceryList, new GroceryItem(dto.Name, category.Id, dto.Quantity, dto.Weight));
        }

        return GroceryListMapper.ToDto(groceryList);
    }

    public async Task DeleteGroceryListAsync(Guid listId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);
        if (groceryList is null) throw new ArgumentNullException(nameof(listId));

        await _groceryRepository.DeleteAsync(groceryList);
    }

    public async Task<GroceryListDto> DeleteGroceryItemFromListAsync(Guid listId, Guid itemId)
    {
        var groceryList = await _groceryRepository.FindAsync(listId);
        if (groceryList is null) throw new ArgumentNullException(nameof(itemId));

        var item = groceryList.GroceryItems.Single(i => i.Id == itemId);
        var res = await _groceryRepository.DeleteGroceryItemAsync(groceryList, item);
        return GroceryListMapper.ToDto(res);
    }
}
