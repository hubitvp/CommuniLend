using CommuniLend.Models.ModelTypes;

namespace CommuniLend.Models
{




    public class ActiveLoanToolIndexViewModel : ToolStatusPropagation
    {
        public required int OpenLoanId { get; set; }

        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required int ToolOwnerId { get; set; }
        public required string ToolOwnerFullName { get; set; }

        public required int ToolBorrowerId { get; set; }

        public required string ToolBorrowerFullName { get; set; }


        public required string LoanDate { get; set; }

        public required string DueDate { get; set; }

    }

}
