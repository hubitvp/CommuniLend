using CommuniLend.Web.ViewModels.Tool;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Services.Contracts
{
    public interface IToolsService
    {
        Task<IEnumerable<IndexViewModel>> GetAllAsync();
        Task<DetailsViewModel?> GetDetailsAsync(int toolId);

        Task<DeleteViewModel?> GetDeleteDetailsAsync(string userId, int toolId);

        Task<bool> DeleteByIdAsync(int toolId);

        Task<AddInputViewModel?> GetAddToolInputViewModelAsync(string userId, AddInputViewModel model);
        IAsyncEnumerable<ValidationResult> ValidateNewToolAsync(AddInputViewModel model);

        Task AddNewToolAsync(string userId, AddInputViewModel model);

        Task<EditInputViewModel?> GetEditModelAsync(int toolId);
    }
}
