namespace CommuniLend.Models
{
    public class MemberViewModel
    {
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int ToolsShared { get; set; }

        public int ActiveLoans { get; set; }

        public string MemberSince { get; set; } = null!;
    }
}
