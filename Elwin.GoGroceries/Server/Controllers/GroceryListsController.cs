using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Core.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Elwin.GoGroceries.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class GroceryListsController : Controller
{
    private readonly ILogger<GroceryListsController> _logger;
    private readonly IManageGroceryLists _manageGroceryLists;

    public GroceryListsController(ILogger<GroceryListsController> logger, IManageGroceryLists manageGroceryLists)
    {
        _logger = logger;
        _manageGroceryLists = manageGroceryLists;
    }

    [HttpGet("{id}")]
    public async Task<GroceryListDto> GetAsync(Guid id)
    {
        return await _manageGroceryLists.GetGroceryListAsync(id);
    }

    [HttpGet]
    public async Task<IEnumerable<GroceryListDto>> GetAllAsync()
    {
        return await _manageGroceryLists.GetAllGroceryLists();
    }

    [HttpPost]
    public async Task<GroceryListDto> PostAsync(GroceryListDto dto)
    {
        return await _manageGroceryLists.AddGroceryListAsync(dto);
    }

    [HttpPost("{id}")]
    public async Task<GroceryListDto> PostGroceryItemAsync(Guid groceryListId, GroceryItemDto dto)
    {
        return await _manageGroceryLists.AddGroceryItemToListAsync(groceryListId, dto);
    }
}
