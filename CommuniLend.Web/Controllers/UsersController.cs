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


        //[HttpGet]
        //public async Task<IActionResult> AddTool(int
        //)
        //{
        //    var model = await iMembersService.GetAddToolInputViewModelAsync(memberId);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> AddTool(int memberId, MemberAddToolInputViewModel model)
        //{
        //    //Member? owner = context.Members.Find(id);

        //    //if (owner == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //bool isCategoryIdValid = context.Categories.Any(x => x.Id == model.InputDataModel.CategoryId);
        //    //if (!isCategoryIdValid)
        //    //{
        //    //    ModelState.AddModelError(nameof(model.InputDataModel.CategoryId), "Category is not valid");
        //    //}

        //    //bool isConditionIdValid = Enum.IsDefined(typeof(ToolCondition), model.InputDataModel.ConditionId);
        //    //if (!isCategoryIdValid)
        //    //{
        //    //    ModelState.AddModelError(nameof(model.InputDataModel.ConditionId), "Condition is not valid");
        //    //}


        //    //if (!ModelState.IsValid)
        //    //{
        //    //    var returnModel = CreateMemberAddToolInputViewModel(owner);
        //    //    return View(returnModel);
        //    //}


        //    bool isAdded = await iMembersService.AddToolAsync(memberId, model);

        //    if (false == isAdded)
        //    {
        //        return BadRequest();
        //    }


        //    return RedirectToAction("Details", new { id });
        //}


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
        //[ActionName("AddMember")]
        //public IActionResult AddMemberGet()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ActionName("AddMember")]
        //public IActionResult AddMemberPost(MemberAddMemberInputModel inputModel)
        //{
        //    if (ModelState.IsValid == false)
        //    {
        //        return View(inputModel);
        //    }

        //    var member = new Member
        //    {
        //        FullName = inputModel.FullName,
        //        Email = inputModel.Email,
        //        PhoneNumber = inputModel.PhoneNumber,
        //        Address = inputModel.Address,
        //        JoinDate = DateOnly.FromDateTime(DateTime.UtcNow),
        //        IsActive = true,

        //    };

        //    context.Members.Add(member);
        //    context.SaveChanges();

        //    return RedirectToAction("Details", new { member.Id });

        //}



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




        //private MemberAddToolInputViewModel CreateMemberAddToolInputViewModel(Member owner)
        //{
        //    var categories = context.Categories
        //        .AsNoTracking()
        //        .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
        //        .ToList();

        //    var conditionOptions = Enum.GetValues<ToolCondition>()
        //        .Select(value => new SelectListItem(value.ToString(), ((int)value).ToString()))
        //        .ToList();

        //    var model = new MemberAddToolInputViewModel
        //    {
        //        OutputDataModel = new OutputDataModel
        //        {
        //            OwnerId = owner.Id,
        //            OwnerFullName = owner.FullName,
        //            Email = owner.Email,
        //            PhoneNumber = owner.PhoneNumber,
        //            MemberSince = owner.JoinDate.ToFormattedString(),
        //            IsActive = owner.IsActive,
        //            Address = owner.Address,

        //            Categories = categories,
        //            ConditionOptions = conditionOptions
        //        }
        //    };

        //    return model;
        //}



    }
}
