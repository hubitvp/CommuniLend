using CommuniLend.Web.ViewModels.User;

namespace CommuniLend.Services.Contracts
{
    public interface IUsersService
    {

        Task<IEnumerable<IndexViewModel>> GetAllAsync();
        Task<DetailsViewModel?> GetDetailsAsync(string userId);


        Task<DetailsViewModel?> GetDashboardDetailsAsync(string userId);


        Task<EditInputViewModel?> EditCurrentDetailsAsync(string userId);


        Task<bool> UpdateUserDetailsAsync(string userId, EditInputViewModel inputModel);


        //Task<MemberAddToolInputViewModel?> GetAddToolInputViewModelAsync(int userId);

        //Task<bool> AddToolAsync(int memberId, MemberAddToolInputViewModel model);

    }
}
