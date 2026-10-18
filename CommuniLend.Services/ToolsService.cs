using CommuniLend.Data;
using CommuniLend.Services.Contracts;
using CommuniLend.Services.Extensions;
using CommuniLend.Web.ViewModels.Tool;
using Microsoft.EntityFrameworkCore;


namespace CommuniLend.Services
{
    public class ToolsService : IToolsService
    {
        private readonly CommuniLendDbContext context;

        public ToolsService(CommuniLendDbContext context)
        {
            this.context = context;
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
                    ToolStatus = x.Status,
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
                ReplacementValue = databaseTool.Tool.ReplacementValue.ToString("C"),
                Condition = databaseTool.Tool.Condition.ToString(),
                OwnerFullName = databaseTool.Tool.Owner.FullName,
                OwnerId = databaseTool.Tool.OwnerId,

                BorrowerId = databaseTool.Tool.OpenLoan?.Borrower?.Id,
                BorrowerFullName = databaseTool.Tool.OpenLoan?.Borrower?.FullName,

                LoanDate = databaseTool.Tool.OpenLoan?.LoanDate.ToFormattedString(),
                DueDate = databaseTool.Tool.OpenLoan?.DueDate.ToFormattedString(),

                ToolStatus = databaseTool.Status,
            };

            return toolViewModel;
        }


        public async Task<DeleteViewModel?> GetDeleteDetailsAsync(int toolId)
        {
            var databaseTool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                .SingleOrDefaultAsync(x => x.Id == toolId);


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

    }
}
