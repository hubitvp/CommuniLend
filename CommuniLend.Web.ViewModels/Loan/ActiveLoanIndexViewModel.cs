using CommuniLend.Web.ViewModels.Enums;
using CommuniLend.Web.ViewModels.User;


namespace CommuniLend.Web.ViewModels.Loan
{



    public class ProvidedToolViewModel
    {
        public required int OpenLoanId { get; set; }

        public required int ToolId { get; set; }

        public required string ToolName { get; set; }


        public required string ToolBorrowerId { get; set; }

        public required string ToolBorrowerFullName { get; set; }


        public required ToolAvailabilityStatus AvailabilityStatus { get; set; }


        public required string LoanDate { get; set; }

        public required string DueDate { get; set; }

        public required string? Notes { get; set; }

    }




    public class BorrowedToolViewModel
    {
        public required int OpenLoanId { get; set; }

        public required int ToolId { get; set; }

        public required string ToolName { get; set; }


        public required string ToolOwnerId { get; set; }

        public required string ToolOwnerFullName { get; set; }


        public required ToolAvailabilityStatus AvailabilityStatus { get; set; }


        public required string LoanDate { get; set; }

        public required string DueDate { get; set; }

        public required string? Notes { get; set; }

    }


    public class ActiveLoanIndexViewModel
    {
        public required IEnumerable<ProvidedToolViewModel> ProvidedTools { get; set; }

        public required IEnumerable<BorrowedToolViewModel> BorrowedTools { get; set; }
    }

}
