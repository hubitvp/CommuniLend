using CommuniLend.Web.ViewModels.Tool;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Services.Contracts
{
    public interface IToolsService
    {
        Task<IEnumerable<IndexViewModel>> GetAllAsync();

        Task<DetailsViewModel?> GetDetailsAsync(int toolId);

        Task<DeleteViewModel?> GetDeleteDetailsAsync(string userId, int toolId);

        Task<bool> DeleteAsync(string userId, int toolId);

        Task<ToolFormInputViewModel?> GetAddModelAsync();

        IAsyncEnumerable<ValidationResult> ValidateInputDataAsync(ToolFormInputViewModel model);

        Task<bool> AddAsync(string userId, ToolFormInputViewModel model);

        Task<ToolFormInputViewModel?> GetEditModelAsync(string userId, int toolId);


        Task<bool> EditAsync(string userId, int toolId, ToolFormInputViewModel model);


    }
}
