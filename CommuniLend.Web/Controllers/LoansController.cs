//using CommuniLend.Data;
//using CommuniLend.Data.Models;
//using CommuniLend.Services.Extensions;
//using CommuniLend.Web.ViewModels.Enums;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace CommuniLend.Web.Controllers
//{
//    public class LoansController : Controller
//    {
//        private readonly CommuniLendDbContext context;

//        public LoansController(CommuniLendDbContext context)
//        {
//            this.context = context;
//        }

//        [HttpGet]
//        public IActionResult Index()
//        {
//            var activeLoans = context.Tools
//                .AsNoTracking()
//                .AsSplitQuery()
//                .Include(x => x.Owner)
//                .Include(x => x.OpenLoan)
//                    .ThenInclude(x => x!.Borrower)
//                .Where(x => x.OpenLoan != null)
//                .WithStatus()
//                .AsEnumerable()
//                .OrderBy(x => x.Status)
//                .ThenBy(x => x.Tool.OpenLoan!.DueDate)
//                .ThenBy(x => x.Tool.Name)
//                .ThenBy(x => x.Tool.Owner.FullName)
//                .ThenBy(x => x.Tool.OpenLoan!.Borrower.FullName)
//                .Select(x => new ActiveLoanToolIndexViewModel
//                {
//                    OpenLoanId = x.Tool.OpenLoan!.Id,
//                    ToolId = x.Tool.Id,
//                    ToolName = x.Tool.Name,
//                    ToolOwnerId = x.Tool.OwnerId,
//                    ToolOwnerFullName = x.Tool.Owner.FullName,
//                    ToolBorrowerId = x.Tool.OpenLoan!.BorrowerId,
//                    ToolBorrowerFullName = x.Tool.OpenLoan!.Borrower.FullName,

//                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
//                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),

//                    Status = x.Status
//                })
//                .ToList();


//            return View(activeLoans);
//        }



//        [HttpPost]
//        public IActionResult Return(int openLoanId)
//        {
//            var openLoan = context.OpenLoans
//                .Include(x => x.Tool)
//                .SingleOrDefault(x => x.Id == openLoanId);
//            if (openLoan == null)
//            {
//                return NotFound();
//            }

//            var closedLoan = new ClosedLoan
//            {
//                ToolId = openLoan.ToolId,
//                OwnerId = openLoan.OwnerId,
//                BorrowerId = openLoan.BorrowerId,
//                LoanDate = openLoan.LoanDate,
//                DueDate = openLoan.DueDate,
//                ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow),
//                Notes = openLoan.Notes
//            };

//            context.ClosedLoans.Add(closedLoan);
//            context.OpenLoans.Remove(openLoan);
//            context.SaveChanges();

//            return RedirectToAction(nameof(Index));
//        }


//        [HttpGet]
//        public IActionResult Create(int toolId)
//        {
//            var databaseTool = context.Tools
//                .AsNoTracking()
//                .AsSplitQuery()
//                .Include(x => x.Category)
//                .Include(x => x.Owner)
//                .Include(x => x.OpenLoan)
//                    .ThenInclude(x => x!.Borrower)
//                .Where(x => x.Id == toolId)
//                .WithStatus()
//                .SingleOrDefault();


//            if (databaseTool == null)
//            {
//                return NotFound();
//            }

//            if (databaseTool.Status != ToolStatus.Available)
//            {
//                return BadRequest();
//            }


//            var loanCreateInputModel = new LoanCreateInputModel
//            {
//                ToolId = databaseTool.Tool.Id,
//                ToolName = databaseTool.Tool.Name,
//                ToolCategoryName = databaseTool.Tool.Category.Name,
//                ToolOwnerId = databaseTool.Tool.OwnerId,
//                ToolOwnerFullName = databaseTool.Tool.Owner.FullName,
//            };
//            return View(loanCreateInputModel);
//        }



//    }
//}
