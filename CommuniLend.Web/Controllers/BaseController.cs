using CommuniLend.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommuniLend.Web.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        public BaseController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        protected string GetUserId()
        {
            return userManager.GetUserId(User) ?? string.Empty;
        }

        protected bool IsUserAuthenticated()
        {
            return User.Identity?.IsAuthenticated ?? false;
        }


    }
}
