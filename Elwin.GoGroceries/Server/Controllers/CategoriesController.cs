using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Core.Managers;
using Elwin.GoGroceries.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Elwin.GoGroceries.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : Controller
    {
        private readonly ILogger<GroceryListsController> _logger;
        private readonly IManageCategories _manageCategories;

        public CategoriesController(ILogger<GroceryListsController> logger, IManageCategories manageCategories)
        {
            _logger = logger;
            _manageCategories = manageCategories;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            return await _manageCategories.GetAllCategoriesAsync();
        }

        [HttpGet]
        [Route("empty")]
        public async Task<IEnumerable<CategoryDto>> GetAllEmptyAsync()
        {
            return await _manageCategories.GetAllCategoriesAsync();
        }

        [HttpPost]
        public async Task<CategoryDto> AddAsync(CategoryDto dto)
        {
            return await _manageCategories.AddCategoryAsync(dto);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<CategoryDto>> DeleteAsync(Guid id)
        {
            await _manageCategories.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
