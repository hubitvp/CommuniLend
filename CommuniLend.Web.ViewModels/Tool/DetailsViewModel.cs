using CommuniLend.Web.ViewModels.Enums;

namespace CommuniLend.Web.ViewModels.Tool
{

    public class DetailsViewModel
    {

        public required int ToolId { get; set; }


        public required string Name { get; set; }

        public required string? ImageUrl { get; set; }

        public required string CategoryName { get; set; }

        public required string Description { get; set; }

        public required string ReplacementValue { get; set; }

        public required string Condition { get; set; }


        public required ToolStatus ToolStatus { get; set; }

        public required string OwnerId { get; set; }
        public required string OwnerFullName { get; set; }



        public required string? BorrowerId { get; set; }
        public required string? BorrowerFullName { get; set; }


        public required string? LoanDate { get; set; }

        public required string? DueDate { get; set; }



    }
}
