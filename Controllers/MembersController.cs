using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace CommuniLend.Controllers
{
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext context;

        public MembersController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {

            var members = context.Members
                .AsNoTracking()
                .AsSplitQuery()
                .Select(x => new MemberIndexViewModel
                {
                    MemberId = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    ToolsOwned = x.OwnedTools.Count,
                    ToolsShared = x.OpenLoansProvidedTools.Count,
                    ToolsBorrowed = x.OpenLoansBorrowedTools.Count,
                    MemberSince = x.JoinDate.ToString("dd MMMM yyyy"),
                    IsActive = x.IsActive
                })
                .ToList();

            return View(members);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {

            var databaseMember = context.Members
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OwnedTools)
                    .ThenInclude(x => x.Category)
                .Include(x => x.OwnedTools)
                    .ThenInclude(x => x.OpenLoan)
                .Include(x => x.OpenLoansProvidedTools)
                    .ThenInclude(x => x.Tool)
                .Include(x => x.OpenLoansProvidedTools)
                    .ThenInclude(x => x.Borrower)
                .Include(x => x.OpenLoansBorrowedTools)
                    .ThenInclude(x => x.Tool)
                .Include(x => x.OpenLoansBorrowedTools)
                    .ThenInclude(x => x.Owner)
                .Include(x => x.ClosedLoansProvidedTools)
                    .ThenInclude(x => x.Tool)
                .Include(x => x.ClosedLoansProvidedTools)
                    .ThenInclude(x => x.Borrower)
                .Include(x => x.ClosedLoansBorrowedTools)
                    .ThenInclude(x => x.Tool)
                .Include(x => x.ClosedLoansBorrowedTools)
                    .ThenInclude(x => x.Owner)
                .SingleOrDefault(x => x.Id == id);


            if (databaseMember == null)
            {
                return NotFound();
            }


            var memberViewModel = new MemberDetailsViewModel
            {
                MemberId = databaseMember.Id,
                FullName = databaseMember.FullName,
                Email = databaseMember.Email,
                PhoneNumber = databaseMember.PhoneNumber,
                Address = databaseMember.Address,
                OwnedTools = databaseMember.OwnedTools
                    .Select(x => new MemberDetailsOwnedToolViewModel
                    {
                        ToolId = x.Id,
                        ToolName = x.Name,
                        CategoryName = x.Category.Name,
                        IsAvailable = x.OpenLoan == null
                    })
                    .ToList(),
                OwnedToolsCount = databaseMember.OwnedTools.Count,


                OpenLoansProvidedTools = databaseMember.OpenLoansProvidedTools
                    .Select(x => new MemberDetailsOpenLoanViewModel
                    {
                        ToolId = x.ToolId,
                        ToolName = x.Tool.Name,
                        LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                        DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                        BorrowerId = x.BorrowerId,
                        BorrowerFullName = x.Borrower.FullName,
                        Status = "On Loan"
                    })
                    .ToList(),

                OpenLoansProvidedToolsCount = databaseMember.OpenLoansProvidedTools.Count,


                OpenLoansBorrowedTools = databaseMember.OpenLoansBorrowedTools
                .Select(x => new MemberDetailsBorrowedToolViewModel
                {
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                    DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                    Status = "On Loan",
                    OwnerId = x.OwnerId,
                    OwnerFullName = x.Owner.FullName,
                })
                .ToList(),
                OpenLoansBorrowedToolsCount = databaseMember.OpenLoansBorrowedTools.Count,


                ClosedLoansProvidedTools = databaseMember.ClosedLoansProvidedTools
                    .Select(x => new MemberDetailsClosedLoanViewModel
                    {
                        ToolId = x.ToolId,
                        ToolName = x.Tool.Name,
                        LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                        DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                        ReturnDate = x.ReturnDate.ToString("dd MMMM yyyy"),
                        Status = "Returned",
                        BorrowerId = x.BorrowerId,
                        BorrowerFullName = x.Borrower.FullName,
                    })
                    .ToList(),
                ClosedLoansProvidedToolsCount = databaseMember.ClosedLoansProvidedTools.Count,

                ClosedLoansBorrowedTools = databaseMember.ClosedLoansBorrowedTools
                    .Select(x => new MemberDetailsClosedBorrowedLoanViewModel
                    {
                        ToolId = x.ToolId,
                        ToolName = x.Tool.Name,
                        LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                        DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                        ReturnDate = x.ReturnDate.ToString("dd MMMM yyyy"),
                        Status = "Returned",
                        OwnerId = x.OwnerId,
                        OwnerFullName = x.Owner.FullName,
                    })
                    .ToList(),
                ClosedLoansBorrowedToolsCount = databaseMember.ClosedLoansBorrowedTools.Count,

                IsActive = databaseMember.IsActive,
                MemberSince = databaseMember.JoinDate.ToString("dd MMMM yyyy")
            };

            return View(memberViewModel);
        }


        [HttpGet]
        public IActionResult AddTool(int id)
        {
            Member? owner = context.Members.Find(id);

            if (owner == null)
            {
                return NotFound();
            }

            MemberAddToolInputViewModel model = CreateMemberAddToolInputViewModel(owner);

            return View(model);
        }


        [HttpPost]
        public IActionResult AddTool(int id, MemberAddToolInputViewModel model)
        {
            Member? owner = context.Members.Find(id);

            if (owner == null)
            {
                return NotFound();
            }

            bool isCategoryIdValid = context.Categories.Any(x => x.Id == model.InputDataModel.CategoryId);
            if (!isCategoryIdValid)
            {
                ModelState.AddModelError(nameof(model.InputDataModel.CategoryId), "Category is not valid");
            }

            bool isConditionIdValid = Enum.IsDefined(typeof(ToolCondition), model.InputDataModel.ConditionId);
            if (!isCategoryIdValid)
            {
                ModelState.AddModelError(nameof(model.InputDataModel.ConditionId), "Condition is not valid");
            }


            if (!ModelState.IsValid)
            {
                var returnModel = CreateMemberAddToolInputViewModel(owner);
                return View(returnModel);
            }


            var databaseModel = new Tool
            {
                Name = model.InputDataModel.Name,
                Description = model.InputDataModel.Description,
                Condition = (ToolCondition)model.InputDataModel.ConditionId,
                ReplacementValue = decimal.Parse(model.InputDataModel.ReplacementValue, CultureInfo.InvariantCulture),
                ImageUrl = model.InputDataModel.ImageUrl,
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = model.InputDataModel.CategoryId,
                OwnerId = id,
            };

            context.Tools.Add(databaseModel);
            context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }


        [HttpGet]
        [ActionName("Edit")]
        public IActionResult EditGet(int id)
        {
            var member = context.Members.Find(id);

            if (member == null)
            {
                return NotFound();
            }

            var model = new MemberEditInputViewModel
            {
                MemberId = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                Address = member.Address,
                IsActive = member.IsActive,

            };

            return View(model);
        }


        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(int id, MemberEditInputViewModel inputModel)
        {

            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }


            var member = context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }


            member.FullName = inputModel.FullName;
            member.Email = inputModel.Email;
            member.PhoneNumber = inputModel.PhoneNumber;
            member.Address = inputModel.Address;
            member.IsActive = inputModel.IsActive;

            context.SaveChanges();

            return RedirectToAction("Details", new { id });
        }


        [HttpGet]
        [ActionName("AddMember")]
        public IActionResult AddMemberGet()
        {
            return View();
        }

        [HttpPost]
        [ActionName("AddMember")]
        public IActionResult AddMemberPost(MemberAddMemberInputModel inputModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(inputModel);
            }

            var member = new Member
            {
                FullName = inputModel.FullName,
                Email = inputModel.Email,
                PhoneNumber = inputModel.PhoneNumber,
                Address = inputModel.Address,
                JoinDate = DateOnly.FromDateTime(DateTime.UtcNow),
                IsActive = true,

            };

            context.Members.Add(member);
            context.SaveChanges();

            return RedirectToAction("Details", new { member.Id });

        }



        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var member = context.Members
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OpenLoansProvidedTools)
                .Include(x => x.OpenLoansBorrowedTools)
                .SingleOrDefault(x => x.Id == id);

            if (member == null)
            {
                return NotFound();
            }


            bool openLoansExist = member.OpenLoansProvidedTools.Count != 0
                || member.OpenLoansBorrowedTools.Count != 0;

            var model = new MemberDeleteViewModel
            {
                MemberId = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                Address = member.Address,
                MemberSince = member.JoinDate.ToString("dd MMMM yyyy"),
                HasActiveLoans = openLoansExist,
                ToolsSharedCount = member.OpenLoansProvidedTools.Count,
                ToolsBorrowedCount = member.OpenLoansBorrowedTools.Count
            };


            return View(model);
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {

            bool openLoansExist = context.OpenLoans.Any(x => x.OwnerId == id || x.BorrowerId == id);

            if (openLoansExist)
            {
                return View();
            }



            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.ClosedLoans
                   .Where(x => x.OwnerId == id || x.BorrowerId == id)
                   .ExecuteDelete();

                context.Tools
                   .Where(x => x.OwnerId == id)
                   .ExecuteDelete();

                context.Members
                   .Where(x => x.Id == id)
                   .ExecuteDelete();


                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }


            return RedirectToAction("Index");
        }




        private MemberAddToolInputViewModel CreateMemberAddToolInputViewModel(Member owner)
        {
            var categories = context.Categories
                .AsNoTracking()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            var conditionOptions = Enum.GetValues<ToolCondition>()
                .Select(value => new SelectListItem(value.ToString(), ((int)value).ToString()))
                .ToList();

            var model = new MemberAddToolInputViewModel
            {
                OutputDataModel = new OutputDataModel
                {
                    OwnerId = owner.Id,
                    OwnerFullName = owner.FullName,
                    Email = owner.Email,
                    PhoneNumber = owner.PhoneNumber,
                    MemberSince = owner.JoinDate.ToString("dd MMMM yyyy"),
                    IsActive = owner.IsActive,
                    Address = owner.Address,

                    Categories = categories,
                    ConditionOptions = conditionOptions
                }
            };

            return model;
        }


    }
}
