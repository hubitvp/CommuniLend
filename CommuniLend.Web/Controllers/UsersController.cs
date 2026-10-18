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

        public UsersController(
            IUsersService usersService,
            UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            iUsersService = usersService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await iUsersService.GetAllAsync();

            return View(users);
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
                return NotFound();
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

            return RedirectToAction(nameof(Dashboard));
        }





        //[HttpGet]
        //[ActionName("Delete")]
        //public IActionResult DeleteGet(int id)
        //{
        //    var member = context.Members
        //        .AsNoTracking()
        //        .AsSplitQuery()
        //        .Include(x => x.ProvidedTools)
        //        .Include(x => x.BorrowedTools)
        //        .SingleOrDefault(x => x.Id == id);

        //    if (member == null)
        //    {
        //        return NotFound();
        //    }


        //    bool openLoansExist = member.ProvidedTools.Count != 0
        //        || member.BorrowedTools.Count != 0;

        //    var model = new MemberDeleteViewModel
        //    {
        //        MemberId = member.Id,
        //        FullName = member.FullName,
        //        Email = member.Email,
        //        PhoneNumber = member.PhoneNumber,
        //        Address = member.Address,
        //        MemberSince = member.JoinDate.ToFormattedString(),
        //        HasActiveLoans = openLoansExist,
        //        ToolsSharedCount = member.ProvidedTools.Count,
        //        ToolsBorrowedCount = member.BorrowedTools.Count
        //    };


        //    return View(model);
        //}


        //[HttpPost]
        //[ActionName("Delete")]
        //public IActionResult DeletePost(int id)
        //{

        //    bool openLoansExist = context.OpenLoans.Any(x => x.OwnerId == id || x.BorrowerId == id);

        //    if (openLoansExist)
        //    {
        //        return View();
        //    }



        //    using var transaction = context.Database.BeginTransaction();

        //    try
        //    {
        //        context.ClosedLoans
        //           .Where(x => x.OwnerId == id || x.BorrowerId == id)
        //           .ExecuteDelete();

        //        context.Tools
        //           .Where(x => x.OwnerId == id)
        //           .ExecuteDelete();

        //        context.Members
        //           .Where(x => x.Id == id)
        //           .ExecuteDelete();


        //        transaction.Commit();
        //    }
        //    catch
        //    {
        //        transaction.Rollback();
        //        throw;
        //    }


        //    return RedirectToAction("Index");
        //}


    }
}
