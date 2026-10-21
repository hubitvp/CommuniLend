using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Services.Contracts;
using CommuniLend.Services.Extensions;
using CommuniLend.Web.ViewModels.Category;
using CommuniLend.Web.ViewModels.Tool;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace CommuniLend.Services
{
    public class ToolsService : IToolsService
    {
        private readonly CommuniLendDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ToolsService(
            CommuniLendDbContext context,
            IWebHostEnvironment environment)
        {
            this.context = context;
            webHostEnvironment = environment;
        }


        public async Task<IEnumerable<IndexViewModel>> GetAllAsync(string userId)
        {
            var tools = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Where(x => x.OwnerId != userId && x.Owner.IsActive)
                .WithStatus()
                .AsAsyncEnumerable()
                .OrderByDescending(x => x.Status)
                .ThenBy(x => x.Tool.Category.Name)
                .ThenBy(x => x.Tool.Owner.FullName)
                .Select(x => new IndexViewModel
                {
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    ToolImageUrl = x.Tool.ImageUrl,
                    CategoryName = x.Tool.Category.Name,
                    OwnerId = x.Tool.OwnerId,
                    OwnerFullName = x.Tool.Owner.FullName,
                    AvailabilityStatus = x.Status,
                })
                .ToListAsync();

            return tools;
        }

        public async Task<DetailsViewModel?> GetDetailsAsync(int toolId)
        {
            var databaseTool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                    .ThenInclude(x => x!.Borrower)
                .Where(tool => tool.Id == toolId)
                .WithStatus()
                .SingleOrDefaultAsync();

            if (databaseTool == null)
            {
                return null;
            }

            var toolViewModel = new DetailsViewModel
            {
                ToolId = databaseTool.Tool.Id,
                Name = databaseTool.Tool.Name,
                ImageUrl = databaseTool.Tool.ImageUrl,
                CategoryName = databaseTool.Tool.Category.Name,
                Description = databaseTool.Tool.Description,
                ReplacementValue = databaseTool.Tool.ReplacementValue.ToString("0.00", CultureInfo.InvariantCulture),
                Condition = databaseTool.Tool.Condition.ToString(),
                OwnerFullName = databaseTool.Tool.Owner.FullName,
                OwnerId = databaseTool.Tool.OwnerId,
                AddedOn = databaseTool.Tool.DateAdded.ToFormattedString(),
                OpenLoanId = databaseTool.Tool.OpenLoan?.Id,
                BorrowerId = databaseTool.Tool.OpenLoan?.Borrower?.Id,
                BorrowerFullName = databaseTool.Tool.OpenLoan?.Borrower?.FullName,

                LoanDate = databaseTool.Tool.OpenLoan?.LoanDate.ToFormattedString(),
                DueDate = databaseTool.Tool.OpenLoan?.DueDate.ToFormattedString(),

                AvailabilityStatus = databaseTool.Status,
            };

            return toolViewModel;
        }


        public async Task<DeleteViewModel?> GetDeleteDetailsAsync(string userId, int toolId)
        {
            var databaseTool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                .SingleOrDefaultAsync(x =>
                    x.Id == toolId
                    && x.OwnerId == userId);

            if (databaseTool == null)
            {
                return null;
            }

            var toolViewModel = new DeleteViewModel
            {
                ToolId = databaseTool.Id,
                ToolName = databaseTool.Name,
                CategoryName = databaseTool.Category.Name,
                AddedOn = databaseTool.DateAdded.ToFormattedString(),
                ToolImageUrl = databaseTool.ImageUrl,
                Condition = databaseTool.Condition.ToString(),
                ReplacementValue = databaseTool.ReplacementValue.ToString("0.00", CultureInfo.InvariantCulture),
                Description = databaseTool.Description,
                IsDeletable = databaseTool.OpenLoan == null,
            };

            return toolViewModel;
        }


        public async Task<bool> DeleteAsync(string userId, int toolId)
        {
            var databaseTool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .SingleOrDefaultAsync(x =>
                    x.Id == toolId
                    && x.OwnerId == userId
                    && x.OpenLoan == null);

            if (databaseTool == null)
            {
                return false;
            }

            bool result = false;

            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                await context.ClosedLoans
                    .Where(x => x.ToolId == toolId)
                    .ExecuteDeleteAsync();


                int rowsAffected = await context.Tools
                    .Where(x =>
                        x.Id == toolId
                        && x.OwnerId == userId
                        && x.OpenLoan == null)
                    .ExecuteDeleteAsync();

                if (rowsAffected == 1)
                {
                    result = true;
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (result)
                {
                    await transaction.CommitAsync();
                    DeleteImage(databaseTool.ImageUrl);
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }


            return result;
        }



        public async Task<ToolFormInputViewModel?> GetAddModelAsync()
        {

            var returnModel = new ToolFormInputViewModel
            {
                Categories = await context.Categories
                        .AsNoTracking()
                        .Select(x => new CategoryViewModel
                        {
                            Id = x.Id,
                            Name = x.Name
                        })
                        .ToListAsync(),
                ConditionOptions = Enum.GetValues<ToolCondition>()
                        .Select(value => new ConditionOptionModel
                        {
                            Id = (int)value,
                            Name = value.ToString()
                        })
                        .ToList(),
            };

            return returnModel;
        }


        public async IAsyncEnumerable<ValidationResult> ValidateInputDataAsync(ToolFormInputViewModel model)
        {
            if (false == await context.Categories.AnyAsync(x => x.Id == model.CategoryId))
            {
                yield return new ValidationResult(
                    "Category is not valid.",
                    [nameof(model.CategoryId)]);
            }

            if (false == Enum.IsDefined(typeof(ToolCondition), model.ConditionId))
            {
                yield return new ValidationResult(
                    "Condition is not valid.",
                    [nameof(model.ConditionId)]);
            }
        }


        public async Task<bool> AddAsync(string userId, ToolFormInputViewModel model)
        {

            var tool = new Tool
            {
                Name = model.ToolName,
                Description = model.Description,
                Condition = (ToolCondition)model.ConditionId,
                ReplacementValue = decimal.Parse(model.ReplacementValue, CultureInfo.InvariantCulture),
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = model.CategoryId,
                ImageUrl = await StoreImage(model),
                OwnerId = userId,
            };


            await context.Tools.AddAsync(tool);
            await context.SaveChangesAsync();

            return true;
        }




        public async Task<ToolFormInputViewModel?> GetEditModelAsync(string userId, int toolId)
        {
            var tool = await context.Tools
                .AsNoTracking()
                .SingleOrDefaultAsync(x =>
                    x.Id == toolId
                    && x.OpenLoan == null
                    && x.OwnerId == userId);

            if (tool == null)
            {
                return null;
            }

            var editModel = new ToolFormInputViewModel
            {
                ToolId = tool.Id,
                ToolName = tool.Name,
                ToolImageUrl = tool.ImageUrl,
                Description = tool.Description,
                CategoryId = tool.CategoryId,
                ConditionId = (int)tool.Condition,
                ReplacementValue = tool.ReplacementValue.ToString("0.00", CultureInfo.InvariantCulture),
                Categories = await context.Categories
                    .AsNoTracking()
                    .Select(x => new CategoryViewModel
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .ToListAsync(),
                ConditionOptions = Enum.GetValues<ToolCondition>()
                        .Select(value => new ConditionOptionModel
                        {
                            Id = (int)value,
                            Name = value.ToString()
                        })
                        .ToList()
            };

            return editModel;
        }







        public async Task<bool> EditAsync(string userId, int toolId, ToolFormInputViewModel model)
        {
            var tool = await context.Tools
                .SingleOrDefaultAsync(x =>
                    x.Id == toolId
                    && x.OwnerId == userId
                    && x.OpenLoan == null);

            if (tool == null)
            {
                return false;
            }

            tool.Name = model.ToolName;
            tool.Description = model.Description;
            tool.Condition = (ToolCondition)model.ConditionId;
            tool.ReplacementValue = decimal.Parse(model.ReplacementValue, CultureInfo.InvariantCulture);
            tool.CategoryId = model.CategoryId;

            var newImageUrl = await StoreImage(model);

            if (newImageUrl != null)
            {
                tool.ImageUrl = newImageUrl;
            }

            await context.SaveChangesAsync();

            return true;
        }


        private async Task<string?> StoreImage(ToolFormInputViewModel model)
        {
            string? imageUrl = null;
            if (model.ToolUploadImage != null && model.ToolUploadImage.File != null)
            {
                string fileName = model.ToolUploadImage.File.FileName;

                var imagePath = Path.Combine(
                    webHostEnvironment.WebRootPath,
                    "images",
                    "tools",
                    fileName);

                while (File.Exists(imagePath))
                {
                    fileName = $"{Guid.NewGuid()}-{model.ToolUploadImage.File.FileName}";

                    imagePath = Path.Combine(
                    webHostEnvironment.WebRootPath,
                    "images",
                    "tools",
                    fileName);
                }

                using (FileStream fileStream = new FileStream(imagePath, FileMode.CreateNew))
                {
                    await model.ToolUploadImage.File.CopyToAsync(fileStream);
                }

                imageUrl = $"/images/tools/{fileName}";
            }
            return imageUrl;
        }

        private void DeleteImage(string? imageUrl)
        {
            if (imageUrl != null)
            {
                var imagePath = Path.Combine(
                    webHostEnvironment.WebRootPath,
                    imageUrl.TrimStart('/', '\\'));

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }
        }

    }
}
