using CommuniLend.Web.ViewModels.User;

namespace CommuniLend.Services.Contracts
{
    public interface IUsersService
    {

        Task<DetailsViewModel?> GetDetailsAsync(string userId);


        Task<DetailsViewModel?> GetDashboardDetailsAsync(string userId);


        Task<EditInputViewModel?> EditCurrentDetailsAsync(string userId);


        Task<bool> UpdateUserDetailsAsync(string userId, EditInputViewModel inputModel);
        Task<DeleteViewModel?> GetDeleteModelAsync(string userId);

        Task<bool> DeleteAsync(string userId);
    }
}
