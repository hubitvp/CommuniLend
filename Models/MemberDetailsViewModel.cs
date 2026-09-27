using System.Diagnostics.Eventing.Reader;

namespace CommuniLend.Models
{

    public class MemberDetailsToolViewModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required bool IsAvailable { get; set; }

        public required string CategoryName { get; set; }
    }

    public class MemberDetailsLoanViewModel
    {
        public required string ToolName { get; set; }

        public required string LoanDate { get; set; }
        public required string DueDate { get; set; }
        public required string Status { get; set; }
    }



    public class MemberDetailsViewModel
    {

        public required int Id { get; set; }


        public required string FullName { get; set; }


        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }

        public required string? Address { get; set; }

        public required string MemberSince { get; set; }

        public required bool IsActive { get; set; }


        public required int ToolsCount { get; set; }

        public required int LoansCount { get; set; }

        public required IEnumerable<MemberDetailsToolViewModel> Tools { get; set; }

        public required IEnumerable<MemberDetailsLoanViewModel> Loans { get; set; }
    }
}
