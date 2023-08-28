using Elwin.GoGroceries.Contracts.Post;
using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Core.Managers;
using Elwin.GoGroceries.Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Elwin.GoGroceries.Contracts.Product;

namespace Elwin.GoGroceries.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TemplatesController : Controller
    {
        private readonly ILogger<GroceryListsController> _logger;
        private readonly IManageTemplates _manageTemplates;

        public TemplatesController(ILogger<GroceryListsController> logger, IManageTemplates manageTemplates)
        {
            _logger = logger;
            _manageTemplates = manageTemplates;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateDto>> GetAsync(Guid id)
        {
            return await _manageTemplates.GetTemplateAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<TemplateDto>> GetAllAsync()
        {
            return await _manageTemplates.GetAllTemplatesAsync();
        }

        [HttpGet("checkProductExists/{id}/{name}/{categoryId}")]
        public async Task<bool> GetDoesProductExistAsync(Guid id, string name, Guid categoryId)
        {
            return await _manageTemplates.DoesTemplateProductExistAsync(id, name, categoryId);
        }

        [HttpPost]
        public async Task<ActionResult<TemplateDto>> PostAsync(TemplateDto dto)
        {
            return await _manageTemplates.AddTemplateAsync(dto);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<TemplateDto>> PostProductAsync(Guid id, PostProductDto dto)
        {
            return await _manageTemplates.AddProductToTemplateAsync(id, dto);
        }

        [HttpPut]
        public async Task<ActionResult<TemplateDto>> PutTemplateUpdateAsync(TemplateDto dto)
        {
            return await _manageTemplates.PutTemplateUpdateAsync(dto);
        }

        [HttpPut("product/{Id}")]
        public async Task<ActionResult<TemplateProductDto>> PutProductUpdateAsync(Guid Id, PostProductDto dto)
        {
            return await _manageTemplates.PutProductUpdate(Id, dto);
        }

        [HttpPut("toGroceryList/{Id}")]
        public async Task<ActionResult> PutToGroceryListAsync(Guid Id)
        {
            await _manageTemplates.ToGroceryListAsync(Id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _manageTemplates.DeleteTemplateAsync(id);
            return Ok();
        }

        [HttpDelete("{listId}/{itemId}")]
        public async Task<ActionResult<TemplateDto>> DeleteAsync(Guid listId, Guid itemId)
        {
            return await _manageTemplates.RemoveProductFromTemplateAsync(listId, itemId);
        }
    }
}
