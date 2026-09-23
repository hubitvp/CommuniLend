using System.Diagnostics.Eventing.Reader;

namespace CommuniLend.Models
{

    public class MemberDetailsToolViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public string CategoryName { get; set; } = null!;
    }

    public class MemberDetailsLoanViewModel
    {
        public string ToolName { get; set; } = null!;

        public string LoanDate { get; set; } = null!;
        public string DueDate { get; set; } = null!;
        public string Status { get; set; } = null!;
    }



    public class MemberDetailsViewModel
    {

        public string FullName { get; set; } = null!;


        public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string MemberSince { get; set; } = null!;

        public bool IsActive { get; set; }


        public int ToolsCount { get; set; }

        public IEnumerable<MemberDetailsToolViewModel> Tools { get; set; } = null!;

        public IEnumerable<MemberDetailsLoanViewModel> Loans { get; set; } = null!;
    }
}
