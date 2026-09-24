using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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

        [HttpGet]
        public IActionResult Create()
        {
            var model = CreateToolCreateInputViewModel();

            return View(model);
        }



        [HttpPost]
        public IActionResult Create(ToolCreateInputViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var returnModel = CreateToolCreateInputViewModel();

                return View(returnModel);
            }

            var databaseModel = new Tool
            {
                Name = model.InputDataModel.Name,
                Description = model.InputDataModel.Description,
                Condition = (ToolCondition)model.InputDataModel.ConditionId,
                ReplacementValue = decimal.Parse(model.InputDataModel.ReplacementValue),
                ImageUrl = model.InputDataModel.ImageUrl,
                IsAvailable = true,
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = model.InputDataModel.CategoryId,
                OwnerId = 1
            };

            context.Tools.Add(databaseModel);
            context.SaveChanges();

            return RedirectToAction("Details", new { id = databaseModel.Id });
        }



        private ToolCreateInputViewModel CreateToolCreateInputViewModel()
        {
            var categories = context.Categories
                .AsNoTracking()
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            var conditionOptions = Enum.GetValues<ToolCondition>()
                .Select(value => new SelectListItem(value.ToString(), ((int)value).ToString()))
                .ToList();

            var model = new ToolCreateInputViewModel
            {
                OutputDataModel = new OutputDataModel
                {
                    Categories = categories,
                    ConditionOptions = conditionOptions
                }
            };

            return model;
        }
    }
}
