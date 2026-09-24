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
            var loans = context.Loans
                .AsNoTracking()
                .Select(x => new LoanIndexViewModel
                {
                    Id = x.Id,
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    BorrowerFullName = x.Borrower.FullName,
                    LoanDate = x.LoanDate.ToString("dd MMMM yyyy"),
                    DueDate = x.DueDate.ToString("dd MMMM yyyy"),
                    Status = x.ReturnDate == null ? LoanStatus.Active : LoanStatus.Returned
                })
                .ToList();

            return View(loans);
        }
    }
}
