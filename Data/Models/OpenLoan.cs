using CommuniLend.DataConstraints;
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


        public virtual Tool Tool { get; set; } = null!;



        [Required]
        public required int BorrowerId { get; set; }


        [ForeignKey(nameof(BorrowerId))]
        public virtual Member Borrower { get; set; } = null!;


        [Required]
        public DateOnly LoanDate { get; set; }


        [Required]
        public DateOnly DueDate { get; set; }


        [StringLength(ValidationConstants.Loan.NotesMaxLength)]
        public string? Notes { get; set; }
    }
}
