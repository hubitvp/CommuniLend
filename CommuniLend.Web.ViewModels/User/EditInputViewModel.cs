using CommuniLend.Data.Models.Constraints;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Web.ViewModels.User
{

    public class EditInputViewModel
    {

        [Required]
        [StringLength(ValidationConstants.User.FullNameMaxLength)]
        [Display(Name = "Full Name")]
        public required string FullName { get; set; }

        [Required]
        [StringLength(ValidationConstants.User.EmailMaxLength)]
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required]
        [StringLength(ValidationConstants.User.PhoneNumberMaxLength)]
        [Phone]
        [Display(Name = "Phone Number")]
        public required string PhoneNumber { get; set; }


        [StringLength(ValidationConstants.User.AddressMaxLength)]
        [Display(Name = "Address")]
        public required string? Address { get; set; }


        [Required]
        [Display(Name = "Is Active")]
        public required bool IsActive { get; set; }
    }



}
