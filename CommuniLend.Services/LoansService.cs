using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Services.Contracts;
using CommuniLend.Services.Extensions;
using CommuniLend.Web.ViewModels.Loan;
using CommuniLend.Web.ViewModels.Tool;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CommuniLend.Services
{
    public class LoansService : ILoansService
    {

        private readonly CommuniLendDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public LoansService(
            CommuniLendDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<ActiveLoanIndexViewModel> GetAllAsync(string userId)
        {
            var providedTools = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OpenLoan)
                    .ThenInclude(x => x!.Borrower)
                .Where(x => x.OwnerId == userId && x.OpenLoan != null)
                .WithStatus()
                .AsAsyncEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.OpenLoan!.DueDate)
                .ThenBy(x => x.Tool.OpenLoan!.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new ProvidedToolViewModel
                {
                    OpenLoanId = x.Tool.OpenLoan!.Id,
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),
                    ToolBorrowerId = x.Tool.OpenLoan!.BorrowerId,
                    ToolBorrowerFullName = x.Tool.OpenLoan!.Borrower.FullName,
                    AvailabilityStatus = x.Status,
                    Notes = x.Tool.OpenLoan.Notes
                })
                .ToListAsync();



            var borrowedTools = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                .Where(x => x.OpenLoan != null && x.OpenLoan.BorrowerId == userId)
                .WithStatus()
                .AsAsyncEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.OpenLoan!.DueDate)
                .ThenBy(x => x.Tool.Owner.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new BorrowedToolViewModel
                {
                    OpenLoanId = x.Tool.OpenLoan!.Id,
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),
                    ToolOwnerId = x.Tool.OwnerId,
                    ToolOwnerFullName = x.Tool.Owner.FullName,
                    AvailabilityStatus = x.Status,
                    Notes = x.Tool.OpenLoan.Notes,
                })
                .ToListAsync();


            var activeLoans = new ActiveLoanIndexViewModel
            {
                ProvidedTools = providedTools,
                BorrowedTools = borrowedTools
            };


            return activeLoans;
        }





        public async Task<bool> ReturnAsync(string userId, int openLoanId)
        {
            var openLoan = await context.OpenLoans
                .AsNoTracking()
                .SingleOrDefaultAsync(x =>
                    x.Id == openLoanId
                    && x.OwnerId == userId);

            if (openLoan == null)
            {
                return false;
            }


            var closedLoan = new ClosedLoan
            {
                ToolId = openLoan.ToolId,
                OwnerId = openLoan.OwnerId,
                BorrowerId = openLoan.BorrowerId,
                LoanDate = openLoan.LoanDate,
                DueDate = openLoan.DueDate,
                ReturnDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Notes = openLoan.Notes,
            };

            await context.ClosedLoans.AddAsync(closedLoan);

            context.OpenLoans.Remove(openLoan);

            await context.SaveChangesAsync();

            return true;
        }



        public async Task<CreateInputViewModel?> GetAddModelAsync(string userId, int toolId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || false == user.IsActive)
            {
                return null;
            }

            var databaseTool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Where(x =>
                    x.Id == toolId
                    && x.Owner.IsActive
                    && x.OwnerId != userId
                    && x.OpenLoan == null)
                .SingleOrDefaultAsync();

            if (databaseTool == null)
            {
                return null;
            }


            var loanCreateInputModel = new CreateInputViewModel
            {
                ToolViewModel = new ToolViewModel
                {
                    ToolId = databaseTool.Id,
                    ToolName = databaseTool.Name,
                    CategoryName = databaseTool.Category.Name,
                    OwnerId = databaseTool.OwnerId,
                    OwnerFullName = databaseTool.Owner.FullName,
                    ImageUrl = databaseTool.ImageUrl,
                    Description = databaseTool.Description,
                    Condition = databaseTool.Condition.ToString(),
                    ReplacementValue = databaseTool.ReplacementValue.ToString("0.00", CultureInfo.InvariantCulture),
                    DateAdded = databaseTool.DateAdded.ToFormattedString(),
                }
            };

            return loanCreateInputModel;

        }



        public async Task<bool> CreateAsync(string userId, int toolId, CreateInputViewModel model)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null || false == user.IsActive)
            {
                return false;
            }

            var databaseTool = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Category)
                .Include(x => x.Owner)
                .Where(x =>
                    x.Id == toolId
                    && x.OwnerId != userId
                    && x.Owner.IsActive
                    && x.OpenLoan == null)
                .WithStatus()
                .SingleOrDefaultAsync();

            if (databaseTool == null)
            {
                return false;
            }

            var newBorrow = new OpenLoan
            {
                ToolId = toolId,
                OwnerId = databaseTool.Tool.OwnerId,
                BorrowerId = userId,
                LoanDate = DateOnly.Parse(model.LoanDate, CultureInfo.InvariantCulture),
                DueDate = DateOnly.Parse(model.DueDate, CultureInfo.InvariantCulture),
                Notes = model.Notes,
            };


            await context.OpenLoans.AddAsync(newBorrow);
            await context.SaveChangesAsync();

            return true;

        }

    }
}
