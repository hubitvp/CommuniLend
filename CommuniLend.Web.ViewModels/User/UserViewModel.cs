namespace CommuniLend.Web.ViewModels.User
{
    public class UserViewModel
    {
        public required string UserId { get; set; }


        public required string FullName { get; set; }


        public required string? Email { get; set; }

        public required string? PhoneNumber { get; set; }

        public required string? Address { get; set; }

        public required string MemberSince { get; set; }

        public required bool IsActive { get; set; }


    }
}
