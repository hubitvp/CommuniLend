using CommuniLend.Data.Models.Constraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommuniLend.Data.Models
{
    [Comment("Active loan of a tool that has not been returned yet")]
    public class OpenLoan
    {

        [Key]
        [Comment("Primary key of the open loan")]
        public int Id { get; set; }

        [Required]
        [Comment("Foreign key to the loaned tool")]
        public required int ToolId { get; set; }


        [ForeignKey(nameof(ToolId))]
        public virtual Tool Tool { get; set; } = null!;


        [Required]
        [StringLength(ValidationConstants.User.ApplicationUserIdMaxLength)]
        [Comment("Foreign key to the member who owns the tool (lender)")]
        public required string OwnerId { get; set; }


        [ForeignKey(nameof(OwnerId))]
        public virtual ApplicationUser Owner { get; set; } = null!;


        [Required]
        [StringLength(ValidationConstants.User.ApplicationUserIdMaxLength)]
        [Comment("Foreign key to the member who borrows the tool")]
        public required string BorrowerId { get; set; }


        [ForeignKey(nameof(BorrowerId))]
        public virtual ApplicationUser Borrower { get; set; } = null!;


        [Required]
        [Comment("Date the tool is handed over to the borrower")]
        public DateOnly LoanDate { get; set; }


        [Required]
        [Comment("Date the tool is expected to be returned")]
        public DateOnly DueDate { get; set; }


        [StringLength(ValidationConstants.Loan.NotesMaxLength)]
        [Comment("Optional notes about the loan")]
        public string? Notes { get; set; }
    }
}
