using CommuniLend.Web.ViewModels.Tool;

namespace CommuniLend.Services.Contracts
{
    public interface IToolsService
    {
        Task<IEnumerable<IndexViewModel>> GetAllAsync();
        Task<DetailsViewModel?> GetDetailsAsync(int toolId);

        Task<DeleteViewModel?> GetDeleteDetailsAsync(int toolId);

        Task<bool> DeleteByIdAsync(int toolId);

    }
}
