using CommuniLend.Data.DataModelConstraints;
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



        public virtual ICollection<Tool> OwnedTools { get; set; } = new List<Tool>();

        public virtual ICollection<OpenLoan> ProvidedTools { get; set; } = new List<OpenLoan>();
        public virtual ICollection<OpenLoan> BorrowedTools { get; set; } = new List<OpenLoan>();


        public virtual ICollection<ClosedLoan> ReturnedProvidedTools { get; set; } = new List<ClosedLoan>();
        public virtual ICollection<ClosedLoan> ReturnedBorrowedTools { get; set; } = new List<ClosedLoan>();

    }
}
