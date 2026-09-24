namespace CommuniLend.Models
{
    public class ToolIndexViewModel
    {


        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string OwnerFullName { get; set; }

        public required string? ImageUrl { get; set; }


        public required bool IsAvailable { get; set; }


        public required string CategoryName { get; set; }


    }
}
