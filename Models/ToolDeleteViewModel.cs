namespace CommuniLend.Models
{
    public class ToolDeleteViewModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string CategoryName { get; set; }


        public required string OwnerFullName { get; set; }

        public required int ActiveLoans { get; set; }
    }
}
