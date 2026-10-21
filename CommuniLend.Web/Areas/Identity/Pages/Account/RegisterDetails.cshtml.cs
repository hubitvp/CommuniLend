using CommuniLend.Data.Models;
using CommuniLend.Data.Models.Constraints;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Web.Areas.Identity.Pages.Account;

/// <summary>Step 2 of registration: collects the rest of the profile and only then saves the user.</summary>
public class RegisterDetailsModel : PageModel
{
    public const string TokenKey = "PendingRegistrationToken";

    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IDataProtectionProvider _dataProtectionProvider;
    private readonly ILogger<RegisterDetailsModel> _logger;

    public RegisterDetailsModel(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IDataProtectionProvider dataProtectionProvider,
        ILogger<RegisterDetailsModel> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _dataProtectionProvider = dataProtectionProvider;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; } = default!;

    public string? ReturnUrl { get; set; }

    public string Email { get; set; } = string.Empty;

    public class InputModel
    {
        [Required]
        [StringLength(ValidationConstants.User.FullNameMaxLength)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = default!;

        [Required]
        [Phone]
        [StringLength(ValidationConstants.User.PhoneNumberMaxLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = default!;

        [StringLength(ValidationConstants.User.AddressMaxLength)]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        /// <summary>Encrypted credentials from step 1.</summary>
        public string? Token { get; set; }
    }

    public IActionResult OnGet(string? returnUrl = null)
    {
        // Peek keeps the token available if the page is refreshed.
        var token = TempData.Peek(TokenKey) as string;
        var pending = RegistrationTokenProtector.Unprotect(_dataProtectionProvider, token);
        if (pending == null)
        {
            return RedirectToPage("Register", new { returnUrl });
        }

        ReturnUrl = returnUrl;
        Email = pending.Email;
        Input = new InputModel { Token = token };
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");
        ReturnUrl = returnUrl;

        var pending = RegistrationTokenProtector.Unprotect(_dataProtectionProvider, Input?.Token);
        if (pending == null)
        {
            // Session expired or token was tampered with - start over.
            return RedirectToPage("Register", new { returnUrl });
        }

        Email = pending.Email;

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = new ApplicationUser
        {
            UserName = pending.Email,
            Email = pending.Email,
            FullName = Input!.FullName.Trim(),
            PhoneNumber = Input.PhoneNumber.Trim(),
            Address = string.IsNullOrWhiteSpace(Input.Address) ? null : Input.Address.Trim(),
            JoinDate = DateOnly.FromDateTime(DateTime.UtcNow),
            IsActive = true
        };

        // The user row is written to the database here, with all data present.
        var result = await _userManager.CreateAsync(user, pending.Password);

        if (result.Succeeded)
        {
            _logger.LogInformation("User created a new account with password.");
            TempData.Remove(TokenKey);

            await _signInManager.SignInAsync(user, isPersistent: false);
            return LocalRedirect(returnUrl);
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return Page();
    }
}
