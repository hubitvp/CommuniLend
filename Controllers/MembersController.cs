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

        public IActionResult Index()
        {

            var members = context.Members
                .AsNoTracking()
                .Select(x => new MemberIndexViewModel
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    ToolsShared = x.OwnedTools.Count,
                    ActiveLoans = x.OpenLoans.Count,
                    MemberSince = x.JoinDate.ToString("dd MMMM yyyy")
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
                .Include(x => x.ClosedLoans)
                .ThenInclude(x => x.Borrower)
                .Include(x => x.ClosedLoans)
                .ThenInclude(x => x.Tool)
                .Include(x => x.OwnedTools)
                .ThenInclude(x => x.Category)
                .Include(x => x.OwnedTools)
                .ThenInclude(x => x.OpenLoan)
                .Include(x => x.OpenLoans)
                .ThenInclude(x => x.Tool)
                .Include(x => x.OpenLoans)
                .ThenInclude(x => x.Borrower)
                .SingleOrDefault(x => x.Id == id);


            if (databaseMember == null)
            {
                return NotFound();
            }


            var borrowedTools = context.OpenLoans
                .AsNoTracking()
                .Where(x => x.BorrowerId == databaseMember.Id)
                .Select(x => new MemberDetailsBorrowedToolViewModel
                {
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                    DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                    Status = "On Loan",
                    OwnerId = x.OwnerId,
                    OwnerFullName = x.Owner.FullName,
                }).ToList();


            var closedBorrowedLoans = context.ClosedLoans
                .AsNoTracking()
                .Where(x => x.BorrowerId == databaseMember.Id)
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

                }).ToList();



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


                OpenLoans = databaseMember.OpenLoans
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

                OpenLoansCount = databaseMember.OpenLoans.Count,


                BorrowedTools = borrowedTools,
                BorrowedToolsCount = borrowedTools.Count,


                ClosedLoans = databaseMember.ClosedLoans
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

                ClosedLoansCount = databaseMember.ClosedLoans.Count,



                ClosedBorrowedLoans = closedBorrowedLoans,
                ClosedBorrowedLoansCount = closedBorrowedLoans.Count,



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
