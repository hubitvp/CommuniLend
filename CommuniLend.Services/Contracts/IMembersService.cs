using CommuniLend.Web.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommuniLend.Services.Contracts
{
    public interface IMembersService
    {

        Task<IEnumerable<MemberIndexViewModel>> GetAllAsync();
        Task<MemberDetailsViewModel?> GetDetailsAsync(int memberId);

        //Task<MemberAddToolInputViewModel?> GetAddToolInputViewModelAsync(int memberId);

        //Task<bool> AddToolAsync(int memberId, MemberAddToolInputViewModel model);

    }
}
