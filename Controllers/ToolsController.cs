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
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .AsSplitQuery()
                .SingleOrDefault(x => x.Id == id);


            if (databaseTool == null)
            {
                return NotFound();
            }


            ClosedLoan? loan = null;
            if (databaseTool.OpenLoan == null)
            {
                loan = context.ClosedLoans
                   .Include(x => x.Borrower)
                   .AsNoTracking()
                   .SingleOrDefault(x => x.ToolId == databaseTool.Id);
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
                IsInUseBy = loan?.Borrower.FullName
            };

            return View(toolViewModel);
        }




        [HttpGet]
        [ActionName("Delete")]
        public IActionResult DeleteGet(int id)
        {
            var databaseTool = context.Tools
                .AsNoTracking()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .AsSplitQuery()
                .SingleOrDefault(x => x.Id == id);


            if (databaseTool == null)
            {
                return NotFound();
            }

            int loansCount = context.OpenLoans.Count(x => x.ToolId == databaseTool.Id);

            var toolViewModel = new ToolDeleteViewModel
            {
                Id = databaseTool.Id,
                Name = databaseTool.Name,
                CategoryName = databaseTool.Category.Name,
                OwnerFullName = databaseTool.Owner.FullName,
                ActiveLoans = loansCount,
            };

            return View(toolViewModel);
        }




        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            context.Tools.Where(x => x.Id == id).ExecuteDelete();

            return RedirectToAction("Index", "Tools");
        }



    }
}
