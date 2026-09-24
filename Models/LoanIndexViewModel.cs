namespace CommuniLend.Models
{


    public enum LoanStatus
    {
        Active,
        Overdue,
        Returned
    }

    public class LoanIndexViewModel
    {
        public required int Id { get; set; }

        public required int ToolId { get; set; }

        public required string ToolName { get; set; } 

        public required string BorrowerFullName { get; set; } 

        public required string LoanDate { get; set; } 

        public required string DueDate { get; set; } 

        public required LoanStatus Status { get; set; }
    }


}
