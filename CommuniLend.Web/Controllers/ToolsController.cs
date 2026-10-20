using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Constraints;
using CommuniLend.Services.Contracts;
using CommuniLend.Web.ViewModels.Tool;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommuniLend.Web.Controllers
{
    public class ToolsController : BaseController
    {
        private readonly IToolsService iToolsService;

        public ToolsController(
            IToolsService toolsService,
            UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            iToolsService = toolsService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var tools = await iToolsService.GetAllAsync(userId);

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
        public async Task<IActionResult> Add()
        {
            var addToolInputViewModel = await iToolsService.GetAddModelAsync();

            return View(addToolInputViewModel);
        }


        [HttpPost]
        [RequestSizeLimit(ValidationConstants.Tool.PictureMaxFileSize)]
        [RequestFormLimits(MultipartBodyLengthLimit = ValidationConstants.Tool.PictureMaxFileSize)]
        public async Task<IActionResult> Add(ToolFormInputViewModel model)
        {
            await foreach (var validationResult in iToolsService.ValidateInputDataAsync(model))
            {
                foreach (var member in validationResult.MemberNames)
                {
                    ModelState.AddModelError(member, validationResult.ErrorMessage ?? string.Empty);
                }
            }

            if (false == ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();

            bool result = await iToolsService.AddAsync(userId, model);

            if (false == result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(UsersController.Dashboard), "Users");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteDetails([FromRoute(Name = "id")] int toolId)
        {
            var userId = GetUserId();
            var toolViewModel = await iToolsService.GetDeleteDetailsAsync(userId, toolId);

            if (toolViewModel == null)
            {
                return NotFound();
            }

            return View(toolViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteConfirm([FromRoute(Name = "id")] int toolId)
        {

            var userId = GetUserId();

            bool isDeleted = await iToolsService.DeleteAsync(userId, toolId);

            if (false == isDeleted)
            {
                return Conflict();
            }

            return RedirectToAction("Dashboard", "Users");
        }


        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] int toolId)
        {

            var userId = GetUserId();

            var model = await iToolsService.GetEditModelAsync(userId, toolId);

            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] int toolId, ToolFormInputViewModel model)
        {
            await foreach (var validationResult in iToolsService.ValidateInputDataAsync(model))
            {
                foreach (var member in validationResult.MemberNames)
                {
                    ModelState.AddModelError(member, validationResult.ErrorMessage ?? string.Empty);
                }
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();
            var result = await iToolsService.EditAsync(userId, toolId, model);

            if (false == result)
            {
                return BadRequest();
            }

            return RedirectToAction("Dashboard", "Users");
        }
    }
}
