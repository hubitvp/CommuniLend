namespace CommuniLend.Models
{
    public class ToolIndexViewModel
    {


        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string OwnerFullName { get; set; } = null!;

        public string? ImageUrl { get; set; }


        public bool IsAvailable { get; set; }


        public string CategoryName { get; set; } = null!;
    }
}
