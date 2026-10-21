namespace CommuniLend.Web.ViewModels.Tool
{
    public class ToolViewModel
    {

        public required int ToolId { get; set; }


        public required string ToolName { get; set; }


        public required string Description { get; set; }


        public required string Condition { get; set; }


        public required string ReplacementValue { get; set; }


        public required string? ImageUrl { get; set; }


        public required string DateAdded { get; set; }


        public required string CategoryName { get; set; } = null!;



        public required string OwnerId { get; set; }
        public required string OwnerFullName { get; set; }


    }
}
