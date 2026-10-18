using CommuniLend.Data.Models.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommuniLend.Data.Models
{
    public class OpenLoan
    {

        [Key]
        public int Id { get; set; }


        [Required]
        public required int ToolId { get; set; }

        [ForeignKey(nameof(ToolId))]
        public virtual Tool Tool { get; set; } = null!;

        [Required]
        [StringLength(ValidationConstants.User.ApplicationUserIdMaxLength)]
        public required string OwnerId { get; set; }


        [ForeignKey(nameof(OwnerId))]
        public virtual ApplicationUser Owner { get; set; } = null!;



        [Required]
        [StringLength(ValidationConstants.User.ApplicationUserIdMaxLength)]

        public required string BorrowerId { get; set; }


        [ForeignKey(nameof(BorrowerId))]
        public virtual ApplicationUser Borrower { get; set; } = null!;


        [Required]
        public DateOnly LoanDate { get; set; }


        [Required]
        public DateOnly DueDate { get; set; }


        [StringLength(ValidationConstants.Loan.NotesMaxLength)]
        public string? Notes { get; set; }
    }
}
