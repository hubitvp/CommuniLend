using CommuniLend.DataConstraints;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace CommuniLend.Models
{
    public class MemberEditInputViewModel
    {
        [Required]
        public required int MemberId { get; set; }

        [Required]
        [StringLength(ValidationConstants.Member.FullNameMaxLength)]
        public required string FullName { get; set; }

        [Required]
        [StringLength(ValidationConstants.Member.EmailMaxLength)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(ValidationConstants.Member.PhoneNumberMaxLength)]
        [Phone]
        public required string PhoneNumber { get; set; }


        [StringLength(ValidationConstants.Member.AddressMaxLength)]
        public required string? Address { get; set; }


        [Required]
        public required bool IsActive { get; set; }
    }
}
