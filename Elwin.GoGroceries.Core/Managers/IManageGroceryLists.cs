using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Domain.Models;
using Elwin.GoGroceries.Infrastructure.Repositories;

namespace Elwin.GoGroceries.Core.Managers;

public interface IManageGroceryLists
{
    Task<GroceryListDto> GetGroceryListAsync(Guid id);
    Task<ICollection<GroceryListDto>> GetAllGroceryLists();
    Task<GroceryListDto> AddGroceryListAsync(GroceryListDto dto);
    Task<GroceryListDto> AddGroceryItemToListAsync(Guid groceryListId, GroceryItemDto dto);
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
        var groceryList = await _groceryRepository.FindAsync(dto.Id);

        if (groceryList is not null)
        {
            return GroceryList.ToDto(groceryList);
        }

        //todo: proper validation
        if (string.IsNullOrEmpty(dto?.Name))
        {
            throw new ArgumentNullException(nameof(dto.Name));
        }

        var res = _groceryRepository.Add(new GroceryList(dto.Name));

        return GroceryList.ToDto(res);
    }

    public async Task<GroceryListDto> AddGroceryItemToListAsync(Guid groceryListId, GroceryItemDto dto)
    { 
        var groceryList = await _groceryRepository.FindAsync(groceryListId);

        //todo proper validation
        if (groceryList is null) throw new ArgumentNullException(nameof(groceryListId));
        if (dto.Name is null) throw new ArgumentNullException(nameof(dto.Name));

        groceryList.AddGroceryItem(new GroceryItem(dto.Name));

        return GroceryList.ToDto(groceryList);
    }
}
