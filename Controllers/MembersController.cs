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
                .Select(x => new MemberViewModel
                {
                    FullName = x.FullName,
                    Email = x.Email,
                    ToolsShared = x.Tools.Count(),
                    ActiveLoans = x.Loans.Count(),
                    MemberSince = x.JoinDate.ToString("MMM yyyy")
                })
                .ToList();

            return View(members);
        }
    }
}
