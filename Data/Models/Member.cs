using CommuniLend.DataConstraints;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Data.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(ValidationConstants.Member.FullNameMaxLength)]
        public required string FullName { get; set; }



        [Required]
        [StringLength(ValidationConstants.Member.EmailMaxLength)]
        public required string Email { get; set; }


        [Required]
        [StringLength(ValidationConstants.Member.PhoneNumberMaxLength)]
        public required string PhoneNumber { get; set; }


        [StringLength(ValidationConstants.Member.AddressMaxLength)]
        public string? Address { get; set; }



        [Required]
        public DateOnly JoinDate { get; set; }


        [Required]
        public required bool IsActive { get; set; }


    }
}
