using CommuniLend.Data.Models;
using CommuniLend.Services.Contracts;
using CommuniLend.Web.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CommuniLend.Web.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService iUsersService;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
            : base(userManager)
        {
            iUsersService = usersService;
            this.signInManager = signInManager;
        }



        [HttpGet]
        public async Task<IActionResult> Details([FromRoute(Name = "id")] string userId)
        {
            string currentUserId = GetUserId();

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized();
            }

            if (currentUserId == userId)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            var userViewModel = await iUsersService.GetDetailsAsync(userId);

            if (userViewModel == null)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            return View(userViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            string userId = GetUserId();

            var memberViewModel = await iUsersService.GetDashboardDetailsAsync(userId);

            if (memberViewModel == null)
            {
                return NotFound();
            }

            return View(memberViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var userId = GetUserId();

            var model = await iUsersService.EditCurrentDetailsAsync(userId);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditInputViewModel inputModel)
        {

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var userId = GetUserId();

            bool isUpdated = await iUsersService.UpdateUserDetailsAsync(userId, inputModel);

            if (false == isUpdated)
            {
                return BadRequest();
            }


            var currentUser = await signInManager.UserManager.GetUserAsync(User);
            if (currentUser != null)
            {
                await signInManager.RefreshSignInAsync(currentUser);
            }


            return RedirectToAction(nameof(Dashboard));
        }


        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            var userId = GetUserId();

            var model = await iUsersService.GetDeleteModelAsync(userId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var userId = GetUserId();

            bool isDeleted = await iUsersService.DeleteAsync(userId);

            if (false == isDeleted)
            {
                return RedirectToAction(nameof(Delete));
            }

            await signInManager.SignOutAsync();

            return RedirectToAction(nameof(ToolsController.Index), "Tools");
        }


    }
}
