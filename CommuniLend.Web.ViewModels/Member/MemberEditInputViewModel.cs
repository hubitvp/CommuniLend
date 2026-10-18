using CommuniLend.Data.Models.Constraints;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Web.ViewModels.Member
{

    public class MemberEditInputViewModel
    {
        public required int MemberId { get; set; }

        [Required]
        [StringLength(ValidationConstants.Member.FullNameMaxLength)]
        [Display(Name = "Full Name")]
        public required string FullName { get; set; }

        [Required]
        [StringLength(ValidationConstants.Member.EmailMaxLength)]
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required]
        [StringLength(ValidationConstants.Member.PhoneNumberMaxLength)]
        [Phone]
        [Display(Name = "Phone Number")]
        public required string PhoneNumber { get; set; }


        [StringLength(ValidationConstants.Member.AddressMaxLength)]
        [Display(Name = "Address")]
        public required string? Address { get; set; }


        [Required]
        [Display(Name = "Is Active")]
        public required bool IsActive { get; set; }
    }



}
