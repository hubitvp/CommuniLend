using CommuniLend.Models.ModelTypes;

namespace CommuniLend.Models
{

    public class MemberDetailsOwnedToolViewModel : ToolStatusPropagation
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string AddedOn { get; set; }

        public required string CategoryName { get; set; }
    }




    public class MemberDetailsProvidedToolViewModel : ToolStatusPropagation
    {
        public required int ToolId { get; set; }


        public required string ToolName { get; set; }



        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }


        public required int BorrowerId { get; set; }

        public required string BorrowerFullName { get; set; }
    }

    public class MemberDetailsBorrowedToolViewModel : ToolStatusPropagation
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }


        public required int OwnerId { get; set; }

        public required string OwnerFullName { get; set; }
    }


    public class MemberDetailsReturnedProvidedToolViewModel
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





    public class MemberDetailsReturnedBorrowedToolViewModel
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


        public required int ProvidedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsProvidedToolViewModel> ProvidedTools { get; set; }


        public required int BorrowedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsBorrowedToolViewModel> BorrowedTools { get; set; }


        public required int ReturnedProvidedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsReturnedProvidedToolViewModel> ReturnedProvidedTools { get; set; }


        public required int ReturnedBorrowedToolsCount { get; set; }
        public required IEnumerable<MemberDetailsReturnedBorrowedToolViewModel> ReturnedBorrowedTools { get; set; }


    }
}
