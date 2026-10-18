using CommuniLend.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CommuniLend.Web.Controllers
{
    public class MembersController : BaseController
    {
        private readonly IMembersService iMembersService;

        public MembersController(
            IMembersService membersService,
            UserManager<IdentityUser> userManager)
            : base(userManager)
        {
            iMembersService = membersService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var members = await iMembersService.GetAllAsync();

            return View(members);
        }



        [HttpGet]
        public async Task<IActionResult> Details([FromRoute(Name = "id")] int memberId)
        {
            var memberViewModel = await iMembersService.GetDetailsAsync(memberId);

            if (memberViewModel == null)
            {
                return NotFound();
            }

            return View(memberViewModel);
        }



        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            string userId = GetUserId();

            var memberViewModel = await iMembersService.GetDashboardDetailsAsync(userId);

            if (memberViewModel == null)
            {
                return NotFound();
            }

            return View(memberViewModel);
        }


        //[HttpGet]
        //public async Task<IActionResult> AddTool(int memberId)
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


        //[HttpGet]
        //[ActionName("Edit")]
        //public IActionResult EditGet(int id)
        //{
        //    var member = context.Members.Find(id);

        //    if (member == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new MemberEditInputViewModel
        //    {
        //        MemberId = member.Id,
        //        FullName = member.FullName,
        //        Email = member.Email,
        //        PhoneNumber = member.PhoneNumber,
        //        Address = member.Address,
        //        IsActive = member.IsActive,

        //    };

        //    return View(model);
        //}


        //[HttpPost]
        //[ActionName("Edit")]
        //public IActionResult EditPost(int id, MemberEditInputViewModel inputModel)
        //{

        //    if (!ModelState.IsValid)
        //    {
        //        return View(inputModel);
        //    }


        //    var member = context.Members.Find(id);
        //    if (member == null)
        //    {
        //        return NotFound();
        //    }


        //    member.FullName = inputModel.FullName;
        //    member.Email = inputModel.Email;
        //    member.PhoneNumber = inputModel.PhoneNumber;
        //    member.Address = inputModel.Address;
        //    member.IsActive = inputModel.IsActive;

        //    context.SaveChanges();

        //    return RedirectToAction("Details", new { id });
        //}


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
