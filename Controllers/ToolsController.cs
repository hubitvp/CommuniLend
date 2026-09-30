using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Globalization;

namespace CommuniLend.Controllers
{
    public class ToolsController : Controller
    {
        private readonly ApplicationDbContext context;

        public ToolsController(ApplicationDbContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
            var tools = context.Tools
                .AsNoTracking()
                .Select(x => new ToolIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    IsAvailable = x.OpenLoan == null,
                    ImageUrl = x.ImageUrl,
                    CategoryName = x.Category.Name,
                    OwnerFullName = x.Owner.FullName
                })
                .ToList();

            return View(tools);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {

            var databaseTool = context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OpenLoan)
                .ThenInclude(x => x!.Borrower)
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .SingleOrDefault(x => x.Id == id);

            if (databaseTool == null)
            {
                return NotFound();
            }

            var toolViewModel = new ToolDetailsViewModel
            {
                Id = databaseTool.Id,
                Name = databaseTool.Name,
                ImageUrl = databaseTool.ImageUrl,
                CategoryName = databaseTool.Category.Name,
                IsAvailable = databaseTool.OpenLoan == null,
                Description = databaseTool.Description,
                ReplacementValue = databaseTool.ReplacementValue.ToString("C"),
                Condition = databaseTool.Condition.ToString(),
                OwnerFullName = databaseTool.Owner.FullName,
                OwnerId = databaseTool.OwnerId,


                BorrowerId = databaseTool.OpenLoan?.Borrower?.Id,
                BorrowerFullName = databaseTool.OpenLoan?.Borrower?.FullName,

                LoanDate = databaseTool.OpenLoan?.LoanDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture),
                DueDate = databaseTool.OpenLoan?.DueDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture),
            };

            return View(toolViewModel);
        }




        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            bool toolExists = context.Tools.Any(x => x.Id == id);

            if (toolExists == false)
            {
                return NotFound();
            }

            var databaseTool = context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                .SingleOrDefault(x => x.Id == id);


            if (databaseTool == null)
            {
                return NotFound();
            }


            var toolViewModel = new ToolDeleteViewModel
            {
                Id = databaseTool.Id,
                Name = databaseTool.Name,
                CategoryName = databaseTool.Category.Name,
                OwnerFullName = databaseTool.Owner.FullName,
                IsLoaned = databaseTool.OpenLoan != null,
            };

            return View(toolViewModel);
        }




        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            int loansCount = context.OpenLoans.Count(x => x.ToolId == id);

            if (loansCount != 0)
            {
                return DeleteGet(id);
            }

            context.Tools.Where(x => x.Id == id).ExecuteDelete();

            return RedirectToAction("Index", "Tools");
        }



    }
}
