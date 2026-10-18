namespace CommuniLend.Web.ViewModels.Member
{
    public class MemberIndexViewModel
    {
        public required int MemberId { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }


        public required int ToolsOwned { get; set; }


        public required int ToolsShared { get; set; }

        public required int ToolsBorrowed { get; set; }

        public required string MemberSince { get; set; }

        public required bool IsActive { get; set; }
    }
}
