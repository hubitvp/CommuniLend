using CommuniLend.Data;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CommuniLend.Controllers
{
    public class LoansController : Controller
    {
        private readonly ApplicationDbContext context;

        public LoansController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var activeLoans = context.OpenLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Select(x => new ActiveLoanIndexViewModel
                {
                    Id = x.Id,
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    ToolOwnerId = x.Tool.OwnerId,
                    ToolOwnerFullName = x.Tool.Owner.FullName,
                    ToolBorrowerId = x.BorrowerId,
                    ToolBorrowerFullName = x.Borrower.FullName,

                    LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                    DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                    //Status = LoanStatus.Active
                })
                .ToList();


            var loans = new LoanIndexViewModel
            {
                ActiveLoans = activeLoans
            };


            return View(loans);
        }
    }
}
