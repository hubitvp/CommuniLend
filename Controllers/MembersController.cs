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
                    ToolsShared = x.Tools.Count(),
                    ActiveLoans = x.Loans.Count(),
                    MemberSince = x.JoinDate.ToString("dd MMMM yyyy")
                })
                .ToList();

            return View(members);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {

            var databaseMember = context.Members
                .Include(x => x.Tools)
                .ThenInclude(x => x.Category)
                .Include(x => x.Loans)
                .ThenInclude(x => x.Tool)
                .AsSplitQuery()
                .SingleOrDefault(x => x.Id == id);


            if (databaseMember == null)
            {
                return NotFound();
            }


            var memberViewModel = new MemberDetailsViewModel
            {
                Id = databaseMember.Id,
                FullName = databaseMember.FullName,
                Email = databaseMember.Email,
                PhoneNumber = databaseMember.PhoneNumber,
                Address = databaseMember.Address,
                Tools = databaseMember.Tools
                    .Select(x => new MemberDetailsToolViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CategoryName = x.Category.Name,
                        IsAvailable = x.IsAvailable
                    })
                    .ToList(),
                ToolsCount = databaseMember.Tools.Count,
                Loans = databaseMember.Loans
                    .Select(x => new MemberDetailsLoanViewModel
                    {
                        ToolName = x.Tool.Name,
                        LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                        DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                        Status = x.ReturnDate.HasValue ? "Returned" : "Active"
                    })
                    .ToList(),
                LoansCount = databaseMember.Loans.Count,
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
                IsAvailable = true,
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = model.InputDataModel.CategoryId,
                OwnerId = id
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
