using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Services.Contracts;
using CommuniLend.Services.Extensions;
using CommuniLend.Web.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CommuniLend.Services
{

    public class UsersService : IUsersService
    {

        private readonly CommuniLendDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IImageStorageService iImageStorage;

        public UsersService(
            CommuniLendDbContext context,
            UserManager<ApplicationUser> userManager,
            IImageStorageService imageStorage)
        {
            this.context = context;
            this.userManager = userManager;
            iImageStorage = imageStorage;
        }




        private async Task<IReadOnlyList<DetailsOwnedToolViewModel>> GetOwnedTools(string userId)
        {
            var ownedTools = await context.Tools
                            .AsNoTracking()
                            .AsSplitQuery()
                            .Include(x => x.Category)
                            .Where(x => x.OwnerId == userId)
                            .WithStatus()
                            .AsAsyncEnumerable()
                            .OrderBy(x => x.Status)
                            .ThenByDescending(x => x.Tool.DateAdded)
                            .ThenBy(x => x.Tool.Category.Name)
                            .ThenBy(x => x.Tool.Name)
                            .Select(x => new DetailsOwnedToolViewModel
                            {
                                ToolId = x.Tool.Id,
                                ToolName = x.Tool.Name,
                                CategoryName = x.Tool.Category.Name,
                                AddedOn = x.Tool.DateAdded.ToFormattedString(),
                                AvailabilityStatus = x.Status,
                            })
                            .ToListAsync();

            return ownedTools;
        }



        private async Task<IReadOnlyList<DetailsProvidedToolViewModel>> GetProvidedTools(string userId)
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
                .Select(x => new DetailsProvidedToolViewModel
                {
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),
                    BorrowerId = x.Tool.OpenLoan!.BorrowerId,
                    BorrowerFullName = x.Tool.OpenLoan!.Borrower.FullName,
                    AvailabilityStatus = x.Status,
                })
                .ToListAsync();

            return providedTools;
        }



        private async Task<IReadOnlyList<DetailsBorrowedToolViewModel>> GetBorrowedTools(string userId)
        {
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
                .Select(x => new DetailsBorrowedToolViewModel
                {
                    ToolId = x.Tool.Id,
                    ToolName = x.Tool.Name,
                    LoanDate = x.Tool.OpenLoan!.LoanDate.ToFormattedString(),
                    DueDate = x.Tool.OpenLoan!.DueDate.ToFormattedString(),
                    OwnerId = x.Tool.OwnerId,
                    OwnerFullName = x.Tool.Owner.FullName,
                    AvailabilityStatus = x.Status,
                })
                .ToListAsync();
            return borrowedTools;
        }



        private async Task<IReadOnlyList<DetailsReturnedProvidedToolViewModel>>
            GetReturnedProvidedTools(string userId)
        {

            var returnedProvidedTools = await context.ClosedLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.OwnerId == userId)
                .OrderByDescending(x => x.ReturnDate)
                .ThenBy(x => x.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new DetailsReturnedProvidedToolViewModel
                {
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    LoanDate = x.LoanDate.ToFormattedString(),
                    DueDate = x.DueDate.ToFormattedString(),
                    ReturnDate = x.ReturnDate.ToFormattedString(),
                    BorrowerId = x.BorrowerId,
                    BorrowerFullName = x.Borrower.FullName,
                    Status = "Returned",
                })
                .ToListAsync();
            return returnedProvidedTools;
        }



        private async Task<IReadOnlyList<DetailsReturnedBorrowedToolViewModel>>
            GetReturnedBorrowedTools(string userId)
        {

            var returnedBorrowedTools = await context.ClosedLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.BorrowerId == userId)
                .OrderByDescending(x => x.ReturnDate)
                .ThenBy(x => x.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new DetailsReturnedBorrowedToolViewModel
                {
                    ToolId = x.ToolId,
                    ToolName = x.Tool.Name,
                    LoanDate = x.LoanDate.ToFormattedString(),
                    DueDate = x.DueDate.ToFormattedString(),
                    ReturnDate = x.ReturnDate.ToFormattedString(),
                    OwnerId = x.OwnerId,
                    OwnerFullName = x.Owner.FullName,
                    Status = "Returned",
                })
                .ToListAsync();
            return returnedBorrowedTools;
        }




        private async Task<UserViewModel?>
            GetUserData(string userId)
        {

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var userData = new UserViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                IsActive = user.IsActive,
                MemberSince = user.JoinDate.ToFormattedString()
            };


            return userData;
        }


        public async Task<DetailsViewModel?> GetDetailsAsync(string userId)
        {

            var userData = await GetUserData(userId);

            if (userData == null)
            {
                return null;
            }

            var ownedTools = await GetOwnedTools(userData.UserId);

            var providedTools = await GetProvidedTools(userData.UserId);

            var borrowedTools = await GetBorrowedTools(userData.UserId);

            var returnedProvidedTools = await GetReturnedProvidedTools(userData.UserId);

            var returnedBorrowedTools = await GetReturnedBorrowedTools(userData.UserId);


            var memberViewModel = new DetailsViewModel
            {
                UserData = userData,
                OwnedTools = ownedTools,
                OwnedToolsCount = ownedTools.Count,
                ProvidedTools = providedTools,
                ProvidedToolsCount = providedTools.Count,
                BorrowedTools = borrowedTools,
                BorrowedToolsCount = borrowedTools.Count,
                ReturnedProvidedTools = returnedProvidedTools,
                ReturnedProvidedToolsCount = returnedProvidedTools.Count,
                ReturnedBorrowedTools = returnedBorrowedTools,
                ReturnedBorrowedToolsCount = returnedBorrowedTools.Count,
            };

            return memberViewModel;
        }




        public async Task<DetailsViewModel?> GetDashboardDetailsAsync(string userId)
        {
            var userData = await GetUserData(userId);

            if (userData == null)
            {
                return null;
            }

            var ownedTools = await GetOwnedTools(userData.UserId);

            var providedTools = await GetProvidedTools(userData.UserId);

            var borrowedTools = await GetBorrowedTools(userData.UserId);

            var returnedProvidedTools = await GetReturnedProvidedTools(userData.UserId);

            var returnedBorrowedTools = await GetReturnedBorrowedTools(userData.UserId);

            var memberViewModel = new DetailsViewModel
            {
                UserData = userData,
                OwnedTools = ownedTools,
                OwnedToolsCount = ownedTools.Count,
                ProvidedTools = providedTools,
                ProvidedToolsCount = providedTools.Count,
                BorrowedTools = borrowedTools,
                BorrowedToolsCount = borrowedTools.Count,
                ReturnedProvidedTools = returnedProvidedTools,
                ReturnedProvidedToolsCount = returnedProvidedTools.Count,
                ReturnedBorrowedTools = returnedBorrowedTools,
                ReturnedBorrowedToolsCount = returnedBorrowedTools.Count,
            };

            return memberViewModel;
        }




        public async Task<EditInputViewModel?> EditCurrentDetailsAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var model = new EditInputViewModel
            {
                FullName = user.FullName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber!,
                Address = user.Address,
                IsActive = user.IsActive,
            };
            return model;
        }


        public async Task<bool> UpdateUserDetailsAsync(string userId, EditInputViewModel inputModel)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            user.FullName = inputModel.FullName;
            user.Email = inputModel.Email;
            user.UserName = inputModel.Email;
            user.PhoneNumber = inputModel.PhoneNumber;
            user.Address = inputModel.Address;
            user.IsActive = inputModel.IsActive;

            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<DeleteViewModel?> GetDeleteModelAsync(string userId)
        {

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            bool hasActiveLoans = await context.OpenLoans.AnyAsync(
                x =>
                x.OwnerId == userId
                || x.BorrowerId == userId);


            var model = new DeleteViewModel
            {
                HasActiveLoans = hasActiveLoans,
            };

            return model;
        }


        public async Task<bool> DeleteAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            bool hasActiveLoans = await context.OpenLoans
                .AnyAsync(x => x.OwnerId == userId || x.BorrowerId == userId);

            if (hasActiveLoans)
            {
                return false;
            }

            var imageUrls = await context.Tools
                .Where(x => x.OwnerId == userId && x.ImageUrl != null)
                .Select(x => x.ImageUrl!)
                .Distinct()
                .ToListAsync();

            var strategy = context.Database.CreateExecutionStrategy();

            bool deleted = await strategy.ExecuteAsync(
                async () =>
                {
                    await using var transaction = await context.Database.BeginTransactionAsync();

                    try
                    {
                        await context.ClosedLoans
                            .Where(x => x.OwnerId == userId || x.BorrowerId == userId)
                            .ExecuteDeleteAsync();

                        await context.Tools
                            .Where(x => x.OwnerId == userId)
                            .ExecuteDeleteAsync();

                        var result = await userManager.DeleteAsync(user);

                        if (false == result.Succeeded)
                        {
                            await transaction.RollbackAsync();
                            return false;
                        }

                        await transaction.CommitAsync();
                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                });

            if (deleted)
            {
                await iImageStorage.DeleteAsync(imageUrls);
            }

            return deleted;

        }

    }
}
