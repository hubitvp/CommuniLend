using CommuniLend.Data;
using CommuniLend.Models;
using CommuniLend.Models.ModelTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public IActionResult Index()
        {
            var tools = context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Owner)
                .Include(x => x.Category)
                .WithStatus()
                .AsEnumerable()
                .OrderByDescending(x => x.Status)
                .ThenBy(x => x.Tool.Category.Name)
                .ThenBy(x => x.Tool.Owner.FullName)
                .Select(x => new ToolIndexViewModel
                {
                    ToolId = x.Tool.Id,
                    Name = x.Tool.Name,
                    ImageUrl = x.Tool.ImageUrl,
                    CategoryName = x.Tool.Category.Name,
                    OwnerId = x.Tool.OwnerId,
                    OwnerFullName = x.Tool.Owner.FullName,
                    Status = x.Status,
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
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                    .ThenInclude(x => x!.Borrower)
                .Where(tool => tool.Id == id)
                .WithStatus()
                .SingleOrDefault();

            if (databaseTool == null)
            {
                return NotFound();
            }

            var toolViewModel = new ToolDetailsViewModel
            {
                ToolId = databaseTool.Tool.Id,
                Name = databaseTool.Tool.Name,
                ImageUrl = databaseTool.Tool.ImageUrl,
                CategoryName = databaseTool.Tool.Category.Name,
                IsAvailable = databaseTool.Tool.OpenLoan == null,
                Description = databaseTool.Tool.Description,
                ReplacementValue = databaseTool.Tool.ReplacementValue.ToString("C"),
                Condition = databaseTool.Tool.Condition.ToString(),
                OwnerFullName = databaseTool.Tool.Owner.FullName,
                OwnerId = databaseTool.Tool.OwnerId,


                BorrowerId = databaseTool.Tool.OpenLoan?.Borrower?.Id,
                BorrowerFullName = databaseTool.Tool.OpenLoan?.Borrower?.FullName,

                LoanDate = databaseTool.Tool.OpenLoan?.LoanDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture),
                DueDate = databaseTool.Tool.OpenLoan?.DueDate.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture),

                Status = databaseTool.Status,

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

            if (databaseTool.OpenLoan != null)
            {
                return View("DeleteNotPossible");
            }


            var toolViewModel = new ToolDeleteViewModel
            {
                ToolId = databaseTool.Id,
                Name = databaseTool.Name,
                CategoryName = databaseTool.Category.Name,
                OwnerFullName = databaseTool.Owner.FullName,
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
