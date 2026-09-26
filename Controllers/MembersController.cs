using CommuniLend.Data;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                FullName = databaseMember.FullName,
                Email = databaseMember.Email,
                PhoneNumber = databaseMember.PhoneNumber,
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
                MemberSince = databaseMember.JoinDate.ToString("MMM yyyy")
            };


            return View(memberViewModel);
        }


    }
}
