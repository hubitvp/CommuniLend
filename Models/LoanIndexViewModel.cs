namespace CommuniLend.Models
{


    public enum LoanStatus
    {
        Active,
        Overdue,
        Returned
    }








    public class ActiveLoanIndexViewModel
    {
        public required int Id { get; set; }

        public required int ToolId { get; set; }

        public required string ToolName { get; set; }

        public required int ToolOwnerId { get; set; }
        public required string ToolOwnerFullName { get; set; }

        public required int ToolBorrowerId { get; set; }

        public required string ToolBorrowerFullName { get; set; }


        public required string LoanDate { get; set; }

        public required string DueDate { get; set; }

        //public required LoanStatus Status { get; set; }
    }





    public class LoanIndexViewModel
    {
        public required IEnumerable<ActiveLoanIndexViewModel> ActiveLoans { get; set; }
    }

}
