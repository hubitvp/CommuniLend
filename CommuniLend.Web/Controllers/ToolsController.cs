using CommuniLend.Data.Models;
using CommuniLend.Services;
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
        [ActionName("Add")]
        public async Task<IActionResult> AddGet(AddInputViewModel? model)
        {
            var userId = GetUserId();

            var addToolInputViewModel = await iToolsService.GetAddToolInputViewModelAsync(userId, model ?? new AddInputViewModel());

            return View(addToolInputViewModel);
        }



        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> AddPost(AddInputViewModel model)
        {
            await foreach (var error in iToolsService.ValidateNewToolAsync(model))
                foreach (var member in error.MemberNames)
                    ModelState.AddModelError(member, error.ErrorMessage!);

            if (!ModelState.IsValid)
            {
                return await AddGet(model);
            }

            await iToolsService.AddNewToolAsync(GetUserId(), model);

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
            bool isDeleted = await iToolsService.DeleteByIdAsync(toolId);

            if (false == isDeleted)
            {
                return Conflict();
            }

            return RedirectToAction("Index", "Tools");
        }




        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] int toolId)
        {
            var model = await iToolsService.GetEditModelAsync(toolId);
            if (model == null) return NotFound();
            return View(model);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit([FromRoute(Name = "id")] int toolId, EditInputViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.OutputDataModel = await iToolsService.GetEditOutputAsync(toolId);   // dropdowns are not posted back
        //        return View(model);
        //    }

        //    var errors = await iToolsService.EditAsync(toolId, GetUserId(), model.InputDataModel);
        //    if (errors.Count > 0)
        //    {
        //        foreach (var e in errors)
        //            foreach (var member in e.MemberNames)
        //                ModelState.AddModelError($"InputDataModel.{member}", e.ErrorMessage!);

        //        model.OutputDataModel = await iToolsService.GetEditOutputAsync(toolId);
        //        return View(model);
        //    }

        //    return RedirectToAction("Details", "Tools", new { id = toolId });
        //}
    }
}
