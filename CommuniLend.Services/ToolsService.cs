using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Services.Contracts;
using CommuniLend.Services.Extensions;
using CommuniLend.Web.ViewModels.Category;
using CommuniLend.Web.ViewModels.Tool;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


namespace CommuniLend.Services
{
    public class ToolsService : IToolsService
    {
        private readonly CommuniLendDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public ToolsService(CommuniLendDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


        public async Task<IEnumerable<IndexViewModel>> GetAllAsync()
        {
            var tools = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Owner)
                .Include(x => x.Category)
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
                .SingleOrDefaultAsync(x => x.Id == toolId && x.OwnerId == userId);

            if (databaseTool == null)
            {
                return null;
            }

            var toolViewModel = new DeleteViewModel
            {
                ToolId = databaseTool.Id,
                Name = databaseTool.Name,
                CategoryName = databaseTool.Category.Name,
                OwnerFullName = databaseTool.Owner.FullName,
                IsDeletable = databaseTool.OpenLoan == null,
            };

            return toolViewModel;
        }


        public async Task<bool> DeleteByIdAsync(int toolId)
        {
            bool hasOpenLoan = await context.OpenLoans.AnyAsync(x => x.ToolId == toolId);

            if (hasOpenLoan)
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
                    .Where(x => x.Id == toolId && x.OpenLoan == null)
                    .ExecuteDeleteAsync();

                if (rowsAffected != 1)
                {
                    await transaction.RollbackAsync();
                    return false;
                }

                await transaction.CommitAsync();
                result = true;
            }
            catch
            {
                await transaction.RollbackAsync();
            }

            return result;
        }



        public async Task<AddInputViewModel?> GetAddToolInputViewModelAsync(string userId, AddInputViewModel model)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var returnModel = new AddInputViewModel
            {
                OutputDataModel = new OutputDataModel
                {
                    OwnerId = user.Id,
                    OwnerFullName = user.FullName,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    MemberSince = user.JoinDate.ToFormattedString(),
                    IsActive = user.IsActive,
                    Address = user.Address,
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
                }
            };
            returnModel.InputDataModel = model.InputDataModel ?? new InputDataModel();

            return returnModel;
        }


        public async IAsyncEnumerable<ValidationResult> ValidateNewToolAsync(AddInputViewModel model)
        {
            var input = model.InputDataModel;

            if (false == await context.Categories.AnyAsync(x => x.Id == input.CategoryId))
            {
                yield return new ValidationResult(
                    "Category is not valid.",
                    [$"{nameof(model.InputDataModel)}.{nameof(input.CategoryId)}"]);
            }

            if (false == Enum.IsDefined(typeof(ToolCondition), input.ConditionId))
            {
                yield return new ValidationResult(
                    "Condition is not valid.",
                    [$"{nameof(model.InputDataModel)}.{nameof(input.ConditionId)}"]);
            }
        }


        public async Task AddNewToolAsync(string userId, AddInputViewModel model)
        {
            var input = model.InputDataModel;

            var tool = new Tool
            {
                Name = input.ToolName,
                Description = input.Description,
                Condition = (ToolCondition)input.ConditionId,
                ReplacementValue = decimal.Parse(input.ReplacementValue, CultureInfo.InvariantCulture),
                ImageUrl = input.ToolImageUrl,
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = input.CategoryId,
                OwnerId = userId
            };

            context.Tools.Add(tool);
            await context.SaveChangesAsync();
        }




        public async Task<EditInputViewModel?> GetEditModelAsync(int toolId)
        {
            var tool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Owner)
                .SingleOrDefaultAsync(t => t.Id == toolId);

            if (tool == null)
            {
                return null;
            }


            var editModel = new EditInputViewModel
            {
                ToolId = tool.Id,
                ToolName = tool.Name,
                Description = tool.Description,
                CategoryId = tool.CategoryId,
                ConditionId = (int)tool.Condition,
                ReplacementValue = tool.ReplacementValue.ToString("0.00", CultureInfo.InvariantCulture),
                OwnerFullName = tool.Owner.FullName,
                AddedOn = tool.DateAdded.ToFormattedString(),
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

    }
}
