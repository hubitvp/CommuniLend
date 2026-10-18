using CommuniLend.Data;
using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Enums;
using CommuniLend.Services.Contracts;
using CommuniLend.Services.Extensions;
using CommuniLend.Web.ViewModels.Category;
using CommuniLend.Web.ViewModels.Member;
using CommuniLend.Web.ViewModels.Tool;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CommuniLend.Services
{
    public class MembersService : IMembersService
    {

        private readonly CommuniLendDbContext context;

        public MembersService(CommuniLendDbContext context)
        {
            this.context = context;
        }




        public async Task<IEnumerable<MemberIndexViewModel>> GetAllAsync()
        {

            var members = await context.Members
                .AsNoTracking()
                .AsSplitQuery()
                .OrderByDescending(x => x.IsActive)
                .ThenBy(x => x.FullName)
                .ThenBy(x => x.OwnedTools.Count)
                .ThenBy(x => x.ProvidedTools.Count)
                .ThenBy(x => x.BorrowedTools.Count)
                .ThenByDescending(x => x.JoinDate)
                .Select(x => new MemberIndexViewModel
                {
                    MemberId = x.Id,
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



        private async Task<IReadOnlyList<MemberDetailsOwnedToolViewModel>> GetOwnedTools(int memberId)
        {
            var ownedTools = await context.Tools
                            .AsNoTracking()
                            .AsSplitQuery()
                            .Include(x => x.Category)
                            .Where(x => x.OwnerId == memberId)
                            .WithStatus()
                            .AsAsyncEnumerable()
                            .OrderBy(x => x.Status)
                            .ThenByDescending(x => x.Tool.DateAdded)
                            .ThenBy(x => x.Tool.Category.Name)
                            .ThenBy(x => x.Tool.Name)
                            .Select(x => new MemberDetailsOwnedToolViewModel
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



        private async Task<IReadOnlyList<MemberDetailsProvidedToolViewModel>> GetProvidedTools(int memberId)
        {
            var providedTools = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.OpenLoan)
                    .ThenInclude(x => x!.Borrower)
                .Where(x => x.OwnerId == memberId && x.OpenLoan != null)
                .WithStatus()
                .AsAsyncEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.OpenLoan!.DueDate)
                .ThenBy(x => x.Tool.OpenLoan!.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsProvidedToolViewModel
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



        private async Task<IReadOnlyList<MemberDetailsBorrowedToolViewModel>> GetBorrowedTools(int memberId)
        {
            var borrowedTools = await context.Tools
                .AsNoTracking()
                .AsSplitQuery()
                .Include(x => x.Owner)
                .Include(x => x.OpenLoan)
                .Where(x => x.OpenLoan != null && x.OpenLoan.BorrowerId == memberId)
                .WithStatus()
                .AsAsyncEnumerable()
                .OrderBy(x => x.Status)
                .ThenByDescending(x => x.Tool.OpenLoan!.DueDate)
                .ThenBy(x => x.Tool.Owner.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsBorrowedToolViewModel
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



        private async Task<IReadOnlyList<MemberDetailsReturnedProvidedToolViewModel>> GetReturnedProvidedTools(int memberId)
        {

            var returnedProvidedTools = await context.ClosedLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.OwnerId == memberId)
                .OrderByDescending(x => x.ReturnDate)
                .ThenBy(x => x.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsReturnedProvidedToolViewModel
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



        private async Task<IReadOnlyList<MemberDetailsReturnedBorrowedToolViewModel>> GetReturnedBorrowedTools(int memberId)
        {

            var returnedBorrowedTools = await context.ClosedLoans
                .AsNoTracking()
                .AsSplitQuery()
                .Where(x => x.BorrowerId == memberId)
                .OrderByDescending(x => x.ReturnDate)
                .ThenBy(x => x.Borrower.FullName)
                .ThenBy(x => x.Tool.Name)
                .Select(x => new MemberDetailsReturnedBorrowedToolViewModel
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


        public async Task<MemberDetailsViewModel?> GetDetailsAsync(int memberId)
        {
            var databaseMember = await context.Members
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == memberId);

            if (databaseMember == null)
            {
                return null;
            }

            var ownedTools = await GetOwnedTools(memberId);

            var providedTools = await GetProvidedTools(memberId);

            var borrowedTools = await GetBorrowedTools(memberId);

            var returnedProvidedTools = await GetReturnedProvidedTools(memberId);

            var returnedBorrowedTools = await GetReturnedBorrowedTools(memberId);

            var memberViewModel = new MemberDetailsViewModel
            {
                MemberId = databaseMember.Id,
                FullName = databaseMember.FullName,
                Email = databaseMember.Email,
                PhoneNumber = databaseMember.PhoneNumber,
                Address = databaseMember.Address,
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
                IsActive = databaseMember.IsActive,
                MemberSince = databaseMember.JoinDate.ToFormattedString()
            };

            return memberViewModel;
        }




        public async Task<AddInputViewModel?> GetAddToolInputViewModelAsync(int memberId)
        {
            var owner = await context.Members
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == memberId);

            if (owner == null)
            {
                return null;
            }

            var model = new AddInputViewModel
            {
                OutputDataModel = new OutputDataModel
                {
                    OwnerId = owner.Id,
                    OwnerFullName = owner.FullName,
                    Email = owner.Email,
                    PhoneNumber = owner.PhoneNumber,
                    MemberSince = owner.JoinDate.ToFormattedString(),
                    IsActive = owner.IsActive,
                    Address = owner.Address,
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

            return model;
        }



        public async Task<bool> AddToolAsync(int memberId, AddInputViewModel model)
        {
            var owner = context.Members.Find(memberId);

            if (owner == null)
            {
                return false;
            }

            bool isCategoryIdValid = context.Categories.Any(x => x.Id == model.InputDataModel.CategoryId);
            if (!isCategoryIdValid)
            {
                //ModelState.AddModelError(nameof(model.InputDataModel.CategoryId), "Category is not valid");
                return false;
            }

            bool isConditionIdValid = Enum.IsDefined(typeof(ToolCondition), model.InputDataModel.ConditionId);
            if (!isCategoryIdValid)
            {
                //ModelState.AddModelError(nameof(model.InputDataModel.ConditionId), "Condition is not valid");
                return false;
            }


            //if (!ModelState.IsValid)
            //{
            //    var returnModel = CreateMemberAddToolInputViewModel(owner);
            //    return View(returnModel);
            //}


            var databaseModel = new Tool
            {
                Name = model.InputDataModel.ToolName,
                Description = model.InputDataModel.Description,
                Condition = (ToolCondition)model.InputDataModel.ConditionId,
                ReplacementValue = decimal.Parse(model.InputDataModel.ReplacementValue, CultureInfo.InvariantCulture),
                ImageUrl = model.InputDataModel.ToolImageUrl,
                DateAdded = DateOnly.FromDateTime(DateTime.UtcNow),
                CategoryId = model.InputDataModel.CategoryId,
                OwnerId = memberId,
            };

            await context.Tools.AddAsync(databaseModel);
            await context.SaveChangesAsync();

            return true;
        }




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
