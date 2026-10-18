using CommuniLend.Data.Models.Constraints;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Data.Models
{

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(ValidationConstants.User.FullNameMaxLength)]
        public string FullName { get; set; } = null!;

        [StringLength(ValidationConstants.User.AddressMaxLength)]
        public string? Address { get; set; }

        [Required]
        public DateOnly JoinDate { get; set; }

        [Required]
        public bool IsActive { get; set; }


        public virtual ICollection<Tool> OwnedTools { get; set; } = new List<Tool>();

        public virtual ICollection<OpenLoan> ProvidedTools { get; set; } = new List<OpenLoan>();
        public virtual ICollection<OpenLoan> BorrowedTools { get; set; } = new List<OpenLoan>();

        public virtual ICollection<ClosedLoan> ReturnedProvidedTools { get; set; } = new List<ClosedLoan>();
        public virtual ICollection<ClosedLoan> ReturnedBorrowedTools { get; set; } = new List<ClosedLoan>();
    }
}
