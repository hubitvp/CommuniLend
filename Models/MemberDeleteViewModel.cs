namespace CommuniLend.Models
{
    public class MemberDeleteViewModel
    {

        public required int MemberId { get; set; }

        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string PhoneNumber { get; set; }


        public required string? Address { get; set; }

        public required string MemberSince { get; set; }


        public required bool HasActiveLoans { get; set; }


        public required int ToolsSharedCount { get; set; }
        public required int ToolsBorrowedCount { get; set; }


    }
}
