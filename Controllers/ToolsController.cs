using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
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
                .AsSplitQuery()
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
            bool isCategoryIdValid = context.Categories.Any(x => x.Id == model.InputDataModel.CategoryId);
            if (!isCategoryIdValid)
            {
                ModelState.AddModelError(nameof(model.InputDataModel.CategoryId), "Category is not valid");
            }

            bool isConditionIdValid = Enum.IsDefined(typeof(ToolCondition), model.InputDataModel.ConditionId);
            if (!isCategoryIdValid)
            {
                ModelState.AddModelError(nameof(model.InputDataModel.ConditionId), "Condition is not valid");
            }

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

            int loansCount = context.Loans.Count(x => x.ToolId == databaseTool.Id);

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
