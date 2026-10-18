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

        public UsersService(CommuniLendDbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }




        public async Task<IEnumerable<IndexViewModel>> GetAllAsync()
        {

            var members = await userManager.Users
                .AsNoTracking()
                .AsSplitQuery()
                .OrderByDescending(x => x.IsActive)
                .ThenBy(x => x.FullName)
                .ThenBy(x => x.OwnedTools.Count)
                .ThenBy(x => x.ProvidedTools.Count)
                .ThenBy(x => x.BorrowedTools.Count)
                .ThenByDescending(x => x.JoinDate)
                .Select(x => new IndexViewModel
                {
                    UserId = x.Id,
                    FullName = x.FullName,
                    Email = x.Email,
                    ToolsOwned = x.OwnedTools.Count,
                    ToolsShared = x.ProvidedTools.Count,
                    ToolsBorrowed = x.BorrowedTools.Count,
                    MemberSince = x.JoinDate.ToFormattedString(),
                    IsActive = x.IsActive
                })
                .ToListAsync();


            return members;
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
                                ToolStatus = x.Status,
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
                    ToolStatus = x.Status,
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
                    ToolStatus = x.Status,
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


        public async Task<DetailsViewModel?> GetDetailsAsync(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return null;
            }

            var ownedTools = await GetOwnedTools(user.Id);

            var providedTools = await GetProvidedTools(user.Id);

            var borrowedTools = await GetBorrowedTools(user.Id);

            var returnedProvidedTools = await GetReturnedProvidedTools(user.Id);

            var returnedBorrowedTools = await GetReturnedBorrowedTools(user.Id);

            var memberViewModel = new DetailsViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
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
                IsActive = user.IsActive,
                MemberSince = user.JoinDate.ToFormattedString()
            };

            return memberViewModel;
        }




        public async Task<DetailsViewModel?> GetDashboardDetailsAsync(string userId)
        {

            ApplicationUser? user = await userManager.FindByIdAsync(userId);


            if (user == null)
            {
                return null;
            }

            var ownedTools = await GetOwnedTools(user.Id);

            var providedTools = await GetProvidedTools(user.Id);

            var borrowedTools = await GetBorrowedTools(user.Id);

            var returnedProvidedTools = await GetReturnedProvidedTools(user.Id);

            var returnedBorrowedTools = await GetReturnedBorrowedTools(user.Id);

            var memberViewModel = new DetailsViewModel
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
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
                IsActive = user.IsActive,
                MemberSince = user.JoinDate.ToFormattedString()
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
                //UserId = user.Id,
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
            user.PhoneNumber = inputModel.PhoneNumber;
            user.Address = inputModel.Address;
            user.IsActive = inputModel.IsActive;

            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }


        //public async Task<AddInputViewModel?> GetAddToolInputViewModelAsync(int memberId)
        //{
        //    var owner = await context.Members
        //        .AsNoTracking()
        //        .SingleOrDefaultAsync(x => x.Id == memberId);

        //    if (owner == null)
        //    {
        //        return null;
        //    }

        //    var model = new AddInputViewModel
        //    {
        //        OutputDataModel = new OutputDataModel
        //        {
        //            OwnerId = owner.Id,
        //            OwnerFullName = owner.FullName,
        //            Email = owner.Email,
        //            PhoneNumber = owner.PhoneNumber,
        //            MemberSince = owner.JoinDate.ToFormattedString(),
        //            IsActive = owner.IsActive,
        //            Address = owner.Address,
        //            Categories = await context.Categories
        //                .AsNoTracking()
        //                .Select(x => new CategoryViewModel
        //                {
        //                    Id = x.Id,
        //                    Name = x.Name
        //                })
        //                .ToListAsync(),
        //            ConditionOptions = Enum.GetValues<ToolCondition>()
        //                .Select(value => new ConditionOptionModel
        //                {
        //                    Id = (int)value,
        //                    Name = value.ToString()
        //                })
        //                .ToList()
        //        }
        //    };

        //    return model;
        //}



        //public async Task<bool> AddToolAsync(int memberId, AddInputViewModel model)
        //{
        //    var owner = context.Members.Find(memberId);

        //    if (owner == null)
        //    {
        //        return false;
        //    }

        //    bool isCategoryIdValid = context.Categories.Any(x => x.Id == model.InputDataModel.CategoryId);
        //    if (!isCategoryIdValid)
        //    {
        //        ModelState.AddModelError(nameof(model.InputDataModel.CategoryId), "Category is not valid");
        //        return false;
        //    }

        //    bool isConditionIdValid = Enum.IsDefined(typeof(ToolCondition), model.InputDataModel.ConditionId);
        //    if (!isCategoryIdValid)
        //    {
        //        ModelState.AddModelError(nameof(model.InputDataModel.ConditionId), "Condition is not valid");
        //        return false;
        //    }


        //    if (!ModelState.IsValid)
        //    {
        //        var returnModel = CreateMemberAddToolInputViewModel(owner);
        //        return View(returnModel);
        //    }


        //    var databaseModel = new Tool
        //    {
        //        Name = model.InputDataModel.ToolName,
        //        Description = model.InputDataModel.Description,
        //        Condition = (ToolCondition)model.InputDataModel.ConditionId,
        //        ReplacementValue = decimal.Parse(model.InputDataModel.ReplacementValue, CultureInfo.InvariantCulture),
        //        ImageUrl = model.InputDataModel.ToolImageUrl,
        //        DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
        //        CategoryId = model.InputDataModel.CategoryId,
        //        OwnerId = memberId,
        //    };

        //    await context.Tools.AddAsync(databaseModel);
        //    await context.SaveChangesAsync();

        //    return true;
        //}




        //private MemberAddToolInputViewModel CreateMemberAddToolInputViewModel(Member owner)
        //{
        //    var categories = context.Categories
        //        .AsNoTracking()
        //        .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
        //        .ToList();

        //    var conditionOptions = Enum.GetValues<ToolCondition>()
        //        .Select(value => new SelectListItem(value.ToString(), ((int)value).ToString()))
        //        .ToList();

        //    var model = new MemberAddToolInputViewModel
        //    {
        //        OutputDataModel = new OutputDataModel
        //        {
        //            OwnerId = owner.Id,
        //            OwnerFullName = owner.FullName,
        //            Email = owner.Email,
        //            PhoneNumber = owner.PhoneNumber,
        //            MemberSince = owner.JoinDate.ToFormattedString(),
        //            IsActive = owner.IsActive,
        //            Address = owner.Address,

        //            Categories = categories,
        //            ConditionOptions = conditionOptions
        //        }
        //    };

        //    return model;
        //}
    }
}
