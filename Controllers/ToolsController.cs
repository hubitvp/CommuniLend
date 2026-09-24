using CommuniLend.Data;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                    IsAvailable = x.IsAvailable,
                    ImageUrl = x.ImageUrl,
                    CategoryName = x.Category.Name,
                    OwnerFullName = x.Owner.FullName
                })
                .ToList();

            return View(tools);
        }



        public IActionResult Details(int id)
        {


            var databaseTool = context.Tools
                .AsNoTracking()
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
                IsAvailable = databaseTool.IsAvailable,
                Description = databaseTool.Description,
                ReplacementValue = databaseTool.ReplacementValue.ToString("C"),
                Condition = databaseTool.Condition.ToString(),
                OwnerFullName = databaseTool.Owner.FullName,
                OwnerId = databaseTool.OwnerId
            };


            return View(toolViewModel);
        }

    }
}
