using CommuniLend.Data.Models.Constraints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Data.Models
{
    [Comment("Registered member of the tool library (extends the Identity user)")]
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(ValidationConstants.User.FullNameMaxLength)]
        [Comment("Full name of the member")]
        public string FullName { get; set; } = null!;

        [StringLength(ValidationConstants.User.AddressMaxLength)]
        [Comment("Optional home address of the member")]
        public string? Address { get; set; }

        [Required]
        [Comment("Date the member joined")]
        public DateOnly JoinDate { get; set; }

        [Required]
        [Comment("Whether the member is active; inactive members' tools are hidden and they cannot borrow")]
        public bool IsActive { get; set; }


        public virtual ICollection<Tool> OwnedTools { get; set; } = new List<Tool>();

        public virtual ICollection<OpenLoan> ProvidedTools { get; set; } = new List<OpenLoan>();
        public virtual ICollection<OpenLoan> BorrowedTools { get; set; } = new List<OpenLoan>();

        public virtual ICollection<ClosedLoan> ReturnedProvidedTools { get; set; } = new List<ClosedLoan>();
        public virtual ICollection<ClosedLoan> ReturnedBorrowedTools { get; set; } = new List<ClosedLoan>();
    }
}
