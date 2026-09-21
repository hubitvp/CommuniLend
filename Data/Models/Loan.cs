using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommuniLend.Data.Models
{
    public class Loan
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public required int ToolId { get; set; }


        [ForeignKey(nameof(ToolId))]
        public virtual Tool Tool { get; set; } = null!;



        [Required]
        public required int BorrowerId { get; set; }


        [ForeignKey(nameof(BorrowerId))]
        public virtual Member Borrower { get; set; } = null!;


        [Required]
        public DateOnly LoanDate { get; set; }


        [Required]
        public DateOnly DueDate { get; set; }



        public DateOnly? ReturnDate { get; set; }


        public string? Notes { get; set; }
    }
}
