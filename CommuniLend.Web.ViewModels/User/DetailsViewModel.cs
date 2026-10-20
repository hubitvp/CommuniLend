using CommuniLend.Web.ViewModels.Enums;


namespace CommuniLend.Web.ViewModels.User
{

    public class DetailsOwnedToolViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string AddedOn { get; set; }

        public required string CategoryName { get; set; }

        public required ToolAvailabilityStatus AvailabilityStatus { get; set; }

    }




    public class DetailsProvidedToolViewModel
    {
        public required int ToolId { get; set; }


        public required string ToolName { get; set; }

        public required ToolAvailabilityStatus AvailabilityStatus { get; set; }


        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }


        public required string BorrowerId { get; set; }

        public required string BorrowerFullName { get; set; }
    }

    public class DetailsBorrowedToolViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }


        public required ToolAvailabilityStatus AvailabilityStatus { get; set; }


        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }


        public required string OwnerId { get; set; }

        public required string OwnerFullName { get; set; }
    }


    public class DetailsReturnedProvidedToolViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string ReturnDate { get; set; }
        public required string Status { get; set; }

        public required string BorrowerId { get; set; }

        public required string BorrowerFullName { get; set; }
    }





    public class DetailsReturnedBorrowedToolViewModel
    {
        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string ReturnDate { get; set; }
        public required string Status { get; set; }

        public required string OwnerId { get; set; }

        public required string OwnerFullName { get; set; }
    }

    public class DetailsViewModel
    {


        public required UserViewModel UserData { get; set; }



        public required int OwnedToolsCount { get; set; }
        public required IEnumerable<DetailsOwnedToolViewModel> OwnedTools { get; set; }


        public required int ProvidedToolsCount { get; set; }
        public required IEnumerable<DetailsProvidedToolViewModel> ProvidedTools { get; set; }


        public required int BorrowedToolsCount { get; set; }
        public required IEnumerable<DetailsBorrowedToolViewModel> BorrowedTools { get; set; }


        public required int ReturnedProvidedToolsCount { get; set; }
        public required IEnumerable<DetailsReturnedProvidedToolViewModel> ReturnedProvidedTools { get; set; }


        public required int ReturnedBorrowedToolsCount { get; set; }
        public required IEnumerable<DetailsReturnedBorrowedToolViewModel> ReturnedBorrowedTools { get; set; }

    }
}
