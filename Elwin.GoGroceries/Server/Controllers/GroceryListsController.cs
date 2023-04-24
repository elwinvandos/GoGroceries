using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Contracts.Post;
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
    public async Task<ActionResult<GroceryListDto>> GetAsync(Guid id)
    {
        return await _manageGroceryLists.GetGroceryListAsync(id);
    }

    [HttpGet]
    public async Task<IEnumerable<GroceryListDto>> GetAllAsync()
    {
        return await _manageGroceryLists.GetAllGroceryListsAsync();
    }

    [HttpPost]
    public async Task<ActionResult<GroceryListDto>> PostAsync(GroceryListDto dto)
    {
        return await _manageGroceryLists.AddGroceryListAsync(dto);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<GroceryListDto>> PostGroceryItemAsync(Guid id, PostGroceryItemDto dto)
    {
        return await _manageGroceryLists.AddGroceryItemToListAsync(id, dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _manageGroceryLists.DeleteGroceryListAsync(id);
        return Ok();
    }

    [HttpDelete("{listId}/{itemId}")]
    public async Task<ActionResult<GroceryListDto>> DeleteAsync(Guid listId, Guid itemId)
    {
        return await _manageGroceryLists.DeleteGroceryItemFromListAsync(listId, itemId);
    }
}
