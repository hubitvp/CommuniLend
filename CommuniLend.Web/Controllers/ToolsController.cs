using CommuniLend.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommuniLend.Web.Controllers
{
    public class ToolsController : BaseController
    {
        private readonly IToolsService iToolsService;

        public ToolsController(
            IToolsService toolsService,
            UserManager<IdentityUser> userManager)
            : base(userManager)
        {
            iToolsService = toolsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tools = await iToolsService.GetAllAsync();

            return View(tools);
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute(Name = "id")] int toolId)
        {

            var toolViewModel = await iToolsService.GetDetailsAsync(toolId);

            if (toolViewModel == null)
            {
                return NotFound();
            }

            return View(toolViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> DeleteDetails([FromRoute(Name = "id")] int toolId)
        {
            var toolViewModel = await iToolsService.GetDeleteDetailsAsync(toolId);

            if (toolViewModel == null)
            {
                return NotFound();
            }

            return View(toolViewModel);
        }




        [HttpPost]
        public async Task<IActionResult> DeleteConfirm([FromRoute(Name = "id")] int toolId)
        {
            bool isDeleted = await iToolsService.DeleteByIdAsync(toolId);

            if (false == isDeleted)
            {
                return Conflict();
            }

            return RedirectToAction("Index", "Tools");
        }

    }
}
