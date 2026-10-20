using CommuniLend.Data.Models;
using CommuniLend.Services.Contracts;
using CommuniLend.Web.ViewModels.Loan;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommuniLend.Web.Controllers
{
    public class LoansController : BaseController
    {
        private readonly ILoansService iLoansService;

        public LoansController(
            ILoansService loansService,
            UserManager<ApplicationUser> userManager)
            : base(userManager)
        {
            iLoansService = loansService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var activeLoans = await iLoansService.GetAllAsync(userId);

            return View(activeLoans);
        }



        [HttpPost]
        public async Task<IActionResult> Return([FromRoute(Name = "id")] int openLoanId)
        {
            var userId = GetUserId();

            bool result = await iLoansService.ReturnAsync(userId, openLoanId);

            if (false == result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Create([FromRoute(Name = "id")] int toolId)
        {
            var userId = GetUserId();

            var loanCreateInputModel = await iLoansService.GetAddModelAsync(userId, toolId);
            if (loanCreateInputModel == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(loanCreateInputModel);
        }




        [HttpPost]
        public async Task<IActionResult> Create([FromRoute(Name = "id")] int toolId, CreateInputViewModel model)
        {

            if (false == ModelState.IsValid)
            {
                return View(model);
            }

            var userId = GetUserId();

            bool result = await iLoansService.CreateAsync(userId, toolId, model);


            if (false == result)
            {
                return BadRequest();
            }


            return RedirectToAction(nameof(Index));
        }



    }
}
