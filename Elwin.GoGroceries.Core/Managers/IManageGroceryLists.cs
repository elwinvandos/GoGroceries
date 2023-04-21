using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Core.Extensions;
using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.Repositories;

namespace Elwin.GoGroceries.Core.Managers;

public interface IManageGroceryLists
{
    Task<GroceryListDto> GetGroceryListAsync(Guid id);
    Task<ICollection<GroceryListDto>> GetAllGroceryLists();
    Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto);
    Task<GroceryListDto> AddGroceryItemToListAsync(Guid listId, GroceryItemDto dto);
    Task DeleteGroceryListAsync(Guid listId);
    Task<GroceryListDto> DeleteGroceryItemFromListAsync(Guid listId, Guid itemId);
}

public class ManageGroceryLists : IManageGroceryLists
{
    private readonly IGroceryRepository _groceryRepository;

    public ManageGroceryLists(IGroceryRepository groceryRepository)
    {
        _groceryRepository = groceryRepository;
    }

    public async Task<GroceryListDto> GetGroceryListAsync(Guid id)
    {
        var groceryList = await _groceryRepository.FindAsync(id);

        //todo proper validation
        if (groceryList is null) throw new ArgumentNullException(nameof(id));

        return GroceryList.ToDto(groceryList);
    }

    public async Task<ICollection<GroceryListDto>> GetAllGroceryLists()
    {
        var groceryLists = await _groceryRepository.GetAll();

        var lists = new List<GroceryListDto>();

        foreach(var list in groceryLists)
        {
            lists.Add(GroceryList.ToDto(list));
        }

        return lists;
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

        return GroceryList.ToDto(res);
    }

    public async Task<GroceryListDto> AddGroceryItemToListAsync(Guid listId, GroceryItemDto dto)
    { 
        var groceryList = await _groceryRepository.FindAsync(listId);

        //todo proper validation
        if (groceryList is null) throw new ArgumentNullException(nameof(listId));
        if (string.IsNullOrEmpty(dto.Name)) throw new ArgumentNullException(nameof(dto.Name));

        dto.Name = dto.Name.Capitalize();
        await _groceryRepository.AddGroceryItemAsync(groceryList, new GroceryItem(dto.Name));

        return GroceryList.ToDto(groceryList);
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
        return GroceryList.ToDto(res);
    }
}
