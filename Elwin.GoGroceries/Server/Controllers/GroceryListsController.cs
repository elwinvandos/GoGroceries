﻿using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Contracts.Post;
using Elwin.GoGroceries.Contracts.Product;
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

    [HttpGet("checkProductExists/{id}/{name}/{categoryId}")]
    public async Task<bool> GetDoesProductExistAsync(Guid id, string name, Guid categoryId)
    {
        return await _manageGroceryLists.DoesListProductExistAsync(id, name, categoryId);
    }

    [HttpPost]
    public async Task<ActionResult<GroceryListDto>> PostAsync(GroceryListDto dto)
    {
        return await _manageGroceryLists.AddGroceryListAsync(dto);
    }

    [HttpPut]
    public async Task<ActionResult<GroceryListDto>> PutGroceryListUpdateAsync(GroceryListDto dto)
    {
        return await _manageGroceryLists.UpdateGroceryList(dto);
    }

    [HttpPost("{id}")]
    public async Task<ActionResult<GroceryListDto>> PostProductAsync(Guid id, PostProductDto dto)
    {
        return await _manageGroceryLists.AddProductToListAsync(id, dto);
    }

    [HttpPut("product/{listId}")]
    public async Task<ActionResult<ProductDto>> PutProductUpdateAsync(Guid listId, PostProductDto dto)
    {
        return await _manageGroceryLists.PutProductUpdate(listId, dto);
    }

    [HttpPut("toTemplate/{Id}")]
    public async Task<ActionResult> PutToTemplateAsync(Guid Id)
    {
        await _manageGroceryLists.ToTemplateAsync(Id);
        return Ok();
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
        return await _manageGroceryLists.RemoveProductFromGroceryListAsync(listId, itemId);
    }
}
