using CommuniLend.Web.ViewModels.Loan;

namespace CommuniLend.Services.Contracts
{
    public interface ILoansService
    {
        Task<ActiveLoanIndexViewModel> GetAllAsync(string userId);

        Task<bool> ReturnAsync(string userId, int openLoanId);



        Task<CreateInputViewModel?> GetAddModelAsync(string userId, int toolId);


        Task<bool> CreateAsync(string userId, int toolId, CreateInputViewModel model);

    }
}
