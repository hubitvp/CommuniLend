using CommuniLend.Data.DataModelConstraints;
using System.ComponentModel.DataAnnotations;

namespace CommuniLend.Models
{
    public class LoanCreateInputModel
    {


        public required int ToolId { get; set; }


        public required string ToolName { get; set; }

        public required string ToolCategoryName { get; set; }


        public required int ToolOwnerId { get; set; }


        public required string ToolOwnerFullName { get; set; }






        [Required]
        public int BorrowerId { get; set; }


        [Required]
        public DateOnly LoanDate { get; set; }

        [Required]
        public DateOnly DueDate { get; set; }


        [StringLength(ValidationConstants.Loan.NotesMaxLength)]
        public string? Notes { get; set; }
    }
}
