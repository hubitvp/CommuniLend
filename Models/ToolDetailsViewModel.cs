namespace CommuniLend.Models
{
    public class ToolDetailsViewModel
    {

        public required int ToolId { get; set; }


        public required string Name { get; set; }

        public required string? ImageUrl { get; set; }

        public required string CategoryName { get; set; }

        public required bool IsAvailable { get; set; }


        public required string Description { get; set; }

        public required string ReplacementValue { get; set; }

        public required string Condition { get; set; }



        public required int OwnerId { get; set; }
        public required string OwnerFullName { get; set; }



        public required int? BorrowerId { get; set; }
        public required string? BorrowerFullName { get; set; }




        public required string? LoanDate { get; set; }

        public required string? DueDate { get; set; }


    }
}
