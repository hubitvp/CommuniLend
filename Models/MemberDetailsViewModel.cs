using System.Diagnostics.Eventing.Reader;

namespace CommuniLend.Models
{

    public class MemberDetailsOwnedToolViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required bool IsAvailable { get; set; }

        public required string CategoryName { get; set; }
    }

    public class MemberDetailsBorrowedToolViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string Status { get; set; }


        public required int OwnerId { get; set; }

        public required string OwnerFullName { get; set; }
    }


    public class MemberDetailsOpenLoanViewModel
    {
        public required int ToolId { get; set; }


        public required string ToolName { get; set; }



        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string Status { get; set; }


        public required int BorrowerId { get; set; }

        public required string BorrowerFullName { get; set; }
    }

    public class MemberDetailsClosedLoanViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string ReturnDate { get; set; }
        public required string Status { get; set; }

        public required int BorrowerId { get; set; }

        public required string BorrowerFullName { get; set; }
    }





    public class MemberDetailsClosedBorrowedLoanViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string ReturnDate { get; set; }
        public required string Status { get; set; }

        public required int OwnerId { get; set; }

        public required string OwnerFullName { get; set; }
    }

    public class MemberDetailsViewModel
    {

        public required int MemberId { get; set; }


        public required string FullName { get; set; }


        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string? Address { get; set; }

        public required string MemberSince { get; set; }

        public required bool IsActive { get; set; }




        public required int OwnedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsOwnedToolViewModel> OwnedTools { get; set; }


        public required int OpenLoansProvidedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsOpenLoanViewModel> OpenLoansProvidedTools { get; set; }


        public required int OpenLoansBorrowedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsBorrowedToolViewModel> OpenLoansBorrowedTools { get; set; }


        public required int ClosedLoansProvidedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsClosedLoanViewModel> ClosedLoansProvidedTools { get; set; }


        public required int ClosedLoansBorrowedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsClosedBorrowedLoanViewModel> ClosedLoansBorrowedTools { get; set; }


    }
}
