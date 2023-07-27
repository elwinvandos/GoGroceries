using Elwin.GoGroceries.API.Services;
using Elwin.GoGroceries.Contracts;
using Elwin.GoGroceries.Server.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Elwin.GoGroceries.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : Controller
    {
        private readonly ILogger<GroceryListsController> _logger;
        private readonly IGetAppVersionService _getAppVersionService;

        public AppController(ILogger<GroceryListsController> logger, IGetAppVersionService getAppVersionService)
        {
            _logger = logger;
            _getAppVersionService = getAppVersionService;
        }

        [HttpGet("version")]
        public ActionResult<string> GetVersion()
        {
            return _getAppVersionService.Version;
        }
    }
}
