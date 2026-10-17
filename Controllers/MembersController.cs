using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Models;
using CommuniLend.Models.ModelTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;


namespace CommuniLend.Controllers
{
    public class MembersController : Controller
    {
        private readonly CommuniLendDbContext context;

        public MembersController(CommuniLendDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public IActionResult Index()
        {

            var members = context.Members
                .AsNoTracking()
                .AsSplitQuery()
                .OrderByDescending(x => x.IsActive)
                .ThenBy(x => x.FullName)
                .ThenBy(x => x.OwnedTools.Count)
                .ThenBy(x => x.ProvidedTools.Count)
                .ThenBy(x => x.BorrowedTools.Count)
                .ThenByDescending(x => x.JoinDate)
                .Select(x => new MemberIndexViewModel
                {
                    MemberId = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    ToolsOwned = x.OwnedTools.Count,
                    ToolsShared = x.ProvidedTools.Count,
                    ToolsBorrowed = x.BorrowedTools.Count,
                    MemberSince = x.JoinDate.ToFormattedString(),
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
                .SingleOrDefault(x => x.Id == id);


            if (databaseMember == null)
            {
                return NotFound();
            }


            var ownedTools = context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Where(x => x.OwnerId == id)
                .WithStatus()
                .AsEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.DateAdded)
                .ThenBy(x => x.Tool.Category.Name)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsOwnedToolViewModel
                {
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    CategoryName = x.Tool.Category.Name,
                    AddedOn = x.Tool.DateAdded.ToFormattedString(),
                    Status = x.Status,
                })
                .ToList();


            var providedTools = context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OpenLoan)
                    .ThenInclude(x => x!.Borrower)
                .Where(x => x.OwnerId == id && x.OpenLoan != null)
                .WithStatus()
                .AsEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.OpenLoan!.DueDate)
                .ThenBy(x => x.Tool.OpenLoan!.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsProvidedToolViewModel
                {
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),
                    BorrowerId = x.Tool.OpenLoan!.BorrowerId,
                    BorrowerFullName = x.Tool.OpenLoan!.Borrower.FullName,
                    Status = x.Status,
                })
                .ToList();


            var borrowedTools = context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                .Where(x => x.OpenLoan != null && x.OpenLoan.BorrowerId == id)
                .WithStatus()
                .AsEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.OpenLoan!.DueDate)
                .ThenBy(x => x.Tool.Owner.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsBorrowedToolViewModel
                {
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),
                    OwnerId = x.Tool.OwnerId,
                    OwnerFullName = x.Tool.Owner.FullName,
                    Status = x.Status,
                })
                .ToList();

            var returnedProvidedTools = context.ClosedLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.OwnerId == id)
                .OrderByDescending(x => x.ReturnDate)
                .ThenBy(x => x.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsReturnedProvidedToolViewModel
                {
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    LoanDate = x.LoanDate.ToFormattedString(),
                    DueDate = x.DueDate.ToFormattedString(),
                    ReturnDate = x.ReturnDate.ToFormattedString(),
                    BorrowerId = x.BorrowerId,
                    BorrowerFullName = x.Borrower.FullName,
                    Status = "Returned",
                })
                .ToList();

            var returnedBorrowedTools = context.ClosedLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.BorrowerId == id)
                .OrderByDescending(x => x.ReturnDate)
                .ThenBy(x => x.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsReturnedBorrowedToolViewModel
                {
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    LoanDate = x.LoanDate.ToFormattedString(),
                    DueDate = x.DueDate.ToFormattedString(),
                    ReturnDate = x.ReturnDate.ToFormattedString(),
                    OwnerId = x.OwnerId,
                    OwnerFullName = x.Owner.FullName,
                    Status = "Returned",
                })
                .ToList();

            var memberViewModel = new MemberDetailsViewModel
            {
                MemberId = databaseMember.Id,
                FullName = databaseMember.FullName,
                Email = databaseMember.Email,
                PhoneNumber = databaseMember.PhoneNumber,
                Address = databaseMember.Address,
                OwnedTools = ownedTools,
                OwnedToolsCount = ownedTools.Count,


                ProvidedTools = providedTools,


                ProvidedToolsCount = providedTools.Count,


                BorrowedTools = borrowedTools,
                BorrowedToolsCount = borrowedTools.Count,


                ReturnedProvidedTools = returnedProvidedTools,
                ReturnedProvidedToolsCount = returnedProvidedTools.Count,



                ReturnedBorrowedTools = returnedBorrowedTools,
                ReturnedBorrowedToolsCount = returnedBorrowedTools.Count,

                IsActive = databaseMember.IsActive,
                MemberSince = databaseMember.JoinDate.ToFormattedString()
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
                .Include(x => x.ProvidedTools)
                .Include(x => x.BorrowedTools)
                .SingleOrDefault(x => x.Id == id);

            if (member == null)
            {
                return NotFound();
            }


            bool openLoansExist = member.ProvidedTools.Count != 0
                || member.BorrowedTools.Count != 0;

            var model = new MemberDeleteViewModel
            {
                MemberId = member.Id,
                FullName = member.FullName,
                Email = member.Email,
                PhoneNumber = member.PhoneNumber,
                Address = member.Address,
                MemberSince = member.JoinDate.ToFormattedString(),
                HasActiveLoans = openLoansExist,
                ToolsSharedCount = member.ProvidedTools.Count,
                ToolsBorrowedCount = member.BorrowedTools.Count
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
                    MemberSince = owner.JoinDate.ToFormattedString(),
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
